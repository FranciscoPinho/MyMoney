using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;

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
    }
}
