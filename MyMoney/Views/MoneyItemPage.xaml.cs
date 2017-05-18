﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            }
        }
        async void OnSaveClicked(object sender, EventArgs e)
        {
            var money = (Money)BindingContext;
            money.Cur = ((Currency)this.FindByName<Picker>("currencypick").SelectedItem).Code;
            Debug.WriteLine("WHAT IS THIS");
            await App.Database.SaveMoneyAsync(money);
            await Navigation.PopAsync();
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

        async void OnChangedCurrency(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedValue = picker.Items[picker.SelectedIndex];
            Entry symbol = this.FindByName<Entry>("symbol");
            Currency currency = App.Database.GetCurrencyAsyncName(selectedValue);
            symbol.Text=currency.Symbol;

        }

    }

}