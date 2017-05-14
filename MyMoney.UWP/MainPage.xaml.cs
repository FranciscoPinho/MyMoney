namespace MyMoney.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new MyMoney.App());
        }
    }
}
