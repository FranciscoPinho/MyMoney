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
            Debug.WriteLine("BeforeitemsSource OnAppearing");
            listView.ItemsSource = await App.Database.GetCurrenciesAsync();
        }
        
        async void OnItemAdded(object sender, EventArgs e)
        {
            Debug.WriteLine("I added an item");
            /*
            await Navigation.PushAsync(new TodoItemPage
            {
                BindingContext = new TodoItem()
            });
            */
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((App)App.Current).ResumeAtTodoId = (e.SelectedItem as Currency).ID;
            Debug.WriteLine("setting ResumeAtTodoId = " + (e.SelectedItem as Currency).ID);
            /*
            await Navigation.PushAsync(new TodoItemPage
            {
                BindingContext = e.SelectedItem as TodoItem
            });
            */
        }
    }
}
