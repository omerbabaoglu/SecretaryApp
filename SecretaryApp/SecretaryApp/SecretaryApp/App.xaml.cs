using SecretaryApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecretaryApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ContactsView());
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
