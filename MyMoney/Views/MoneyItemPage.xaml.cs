using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Net;

namespace MyMoney
{
 
    public partial class MoneyItemPage : ContentPage
    {
        public MoneyItemPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtTodoId = -1;
            List<Currency> mycurrencies = await App.Database.GetCurrenciesAsync();
            foreach (Currency cur in mycurrencies)
            {
                this.FindByName<Picker>("currencypick").Items.Add(cur.Code);
                this.FindByName<Picker>("convertpick").Items.Add(cur.Code+" ("+cur.Symbol+")");
            }
            this.FindByName<Picker>("currencypick").SelectedItem = ((Money)BindingContext).Cur;
        }
        async void OnSaveClicked(object sender, EventArgs e)
        {
            Money money = (Money)BindingContext;
            if (this.FindByName<Picker>("currencypick").SelectedIndex != -1) { 
                money.Cur = this.FindByName<Picker>("currencypick").SelectedItem.ToString();
                money.Symbol = this.symbol.Text;
                money.Value = Convert.ToDouble(this.value.Text);
                await App.Database.SaveMoneyAsync(money);
                await Navigation.PopAsync();
            }
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (Money)BindingContext;
            await App.Database.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void OnChangedCurrency(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedValue = picker.Items[picker.SelectedIndex];
            Currency currency = App.Database.GetCurrencyAsyncName(selectedValue);
            this.symbol.Text=currency.Symbol;
            this.cur.Text = selectedValue;
        }

        private void Convert2_Clicked(object sender, EventArgs e)
        {
            Picker picker = this.FindByName<Picker>("currencypick");
            var fromCur = picker.Items[picker.SelectedIndex];
            string targetcurrency = "";
            Picker choice = this.FindByName<Picker>("convertpick");
            if (choice.SelectedIndex == -1)
                return;
            else targetcurrency = choice.Items[choice.SelectedIndex].Substring(0, 3);

            var uri = string.Format("http://download.finance.yahoo.com/d/quotes?f=sl1d1t1&s={0}{1}=X", fromCur, targetcurrency);
            var cb = new AsyncCallback(CallHandler);
            CallWebAsync(uri, lab1, lab2, cb);
        }

        private void calculateSingle()
        {
            Debug.WriteLine("At Calculate Single");
            double total = 0;
            Picker picker = this.FindByName<Picker>("currencypick");
            var fromCur = picker.Items[picker.SelectedIndex];
            string originalcur = fromCur + " (" + this.symbol.Text + ")";
            
            Picker choice = this.FindByName<Picker>("convertpick");
            string targetcurrency = "";
            if (choice.SelectedIndex == -1)
                return;
            else targetcurrency = choice.Items[choice.SelectedIndex].Substring(0, 3);
            Rate rate = App.Database.GetRateAsync(fromCur, targetcurrency);

            Label result = this.FindByName<Label>("lab2");
            if (rate != null)
            {
                Debug.WriteLine(Convert.ToDouble(this.value.Text));
                total += Convert.ToDouble(this.value.Text) * rate.Value;
            }
            else
            {
               result.Text = "Rates for conversion from " + originalcur + " to " + targetcurrency + " were not found, Connect to internet and try again";
               return;
            }
            Debug.WriteLine(total);
            result.Text = "Total: " + Convert.ToString(total) + " " + choice.Items[choice.SelectedIndex];
            
        }

        private void CallWebAsync(string uri, Label status, Label response, AsyncCallback cb)
        {
            var request = HttpWebRequest.Create(uri);
            request.Method = "GET";
            var state = new Tuple<Label, Label, WebRequest>(status, response, request);
            request.BeginGetResponse(cb, state);
        }

        private void CallHandler(IAsyncResult ar)
        {
            try
            {
                var state = (Tuple<Label, Label, WebRequest>)ar.AsyncState;
                var request = state.Item3;

                using (HttpWebResponse response = request.EndGetResponse(ar) as HttpWebResponse)
                {
                    Device.BeginInvokeOnMainThread(() => state.Item1.Text = "Status: " + response.StatusCode);
                    if (response.StatusCode == HttpStatusCode.OK)
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            var content = reader.ReadToEnd();
                            parseYahooRate(content);
                            Device.BeginInvokeOnMainThread(() => calculateSingle());
                        }
                    else
                    {
                        calculateSingle();
                        Device.BeginInvokeOnMainThread(() => state.Item1.Text = "Connection Failed, using local rates: " + response.StatusCode);
                    }
                }
            }
            catch (Exception e)
            {
                Device.BeginInvokeOnMainThread(() => this.lab2.Text = "Status:No Connection");
                Device.BeginInvokeOnMainThread(() => this.lab1.Text = "");
                Device.BeginInvokeOnMainThread(() => calculateSingle());
            }

        }

        public async void parseYahooRate(string content)
        {
            try {
                string[] tokens = content.Split(',');
                string from = tokens[0].Substring(1, 3);
                string to = tokens[0].Substring(4, 3);
                double value = Convert.ToDouble(tokens[1]);
                Rate rate = App.Database.GetRateAsync(from, to);
                if (rate != null)
                {
                    rate.Value = value;
                    await App.Database.SaveRateAsync(rate);
                }
                else
                {
                    rate = new Rate();
                    rate.FromCur = from;
                    rate.Value = value;
                    rate.TargetCur = to;
                    await App.Database.SaveRateAsync(rate);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return;
            }
        }

    }

}