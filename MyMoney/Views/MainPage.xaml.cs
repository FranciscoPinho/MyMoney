using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;
using System.Net;
using System.IO;

namespace MyMoney
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtTodoId = -1;
            listView.ItemsSource = await App.Database.GetMoneyAsync();
            List<Currency> mycurrencies = await App.Database.GetCurrenciesAsync();
            foreach (Currency cur in mycurrencies)
            {
                this.FindByName<Picker>("targetcur").Items.Add(cur.Code);
            }
        }
        
        async void OnItemAdded(object sender, EventArgs e)
        {   
            await Navigation.PushAsync(new MoneyItemPage
            {
                BindingContext = new Money()
            });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((App)App.Current).ResumeAtTodoId = (e.SelectedItem as Money).ID;
            Debug.WriteLine("setting ResumeAtTodoId = " + (e.SelectedItem as Money).ID);
            
            await Navigation.PushAsync(new MoneyItemPage
            {
                BindingContext = e.SelectedItem as Money
            });
            
        }

        private void Convert_Clicked(object sender, EventArgs e)
        {
            string targetcurrency = "";
            Picker choice = this.FindByName<Picker>("targetcur");
            if (choice.SelectedIndex == -1)
                return;
            else targetcurrency = choice.Items[choice.SelectedIndex];
            foreach(Money m in listView.ItemsSource)
            {
                var uri = string.Format("http://download.finance.yahoo.com/d/quotes?f=sl1d1t1&s={0}{1}=X", m.Cur, targetcurrency);
                var cb = new AsyncCallback(CallHandler);
                CallWebAsync(uri, lab1, lab2, cb);
                return;
            }

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
                        Device.BeginInvokeOnMainThread(() => state.Item2.Text = content);
                    }
                else Device.BeginInvokeOnMainThread(() => state.Item1.Text = "Connection Failed, using local rates: " + response.StatusCode);
            }
        }

        public async void parseYahooRate(string content)
        {
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
    }
}
