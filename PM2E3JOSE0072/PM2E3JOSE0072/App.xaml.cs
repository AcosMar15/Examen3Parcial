using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E3JOSE0072.Views;

namespace PM2E3JOSE0072
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ListPagos());
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
