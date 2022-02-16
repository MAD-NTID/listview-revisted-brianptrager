using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HamburgerApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new HamburgersPage();
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
