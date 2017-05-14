using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using System.Diagnostics;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyMoney
{
    public partial class App : Application
    {
        static MyMoneyDatabase database;
        public App()
        {
            Resources = new ResourceDictionary();
            Resources.Add("primaryGreen", Color.FromHex("00CC00"));
            Resources.Add("primaryDarkGreen", Color.FromHex("006600"));

            var nav = new NavigationPage(new MainPage());
            nav.BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"];
            nav.BarTextColor = Color.White;
            MainPage = nav;
        }

        public static MyMoneyDatabase Database
        {
            get
            {
                Debug.WriteLine("Entered database get");
                if (database == null)
                {
                    Debug.WriteLine("Im trying to create new database");
                    database = new MyMoneyDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("MyMoneySQLite.db3"));
                    Debug.WriteLine("I created the database");
                }
                return database;
            }
        }

        public int ResumeAtTodoId { get; set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
