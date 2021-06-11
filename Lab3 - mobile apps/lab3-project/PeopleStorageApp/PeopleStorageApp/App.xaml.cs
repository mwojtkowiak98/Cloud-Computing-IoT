using PeopleStorageApp.DataContracts;
using Xamarin.Forms;

namespace PeopleStorageApp
{
    public partial class App : Application
    {
        private const string API_URL = "http://192.168.1.23:6001/api";

        public App()
        {
            var client = RestEase.RestClient.For<IPeopleClient>(API_URL);

            InitializeComponent();

            MainPage = new MainPage(client);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}