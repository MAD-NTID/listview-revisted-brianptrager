using HamburgerApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HamburgerApp
{
    public partial class HamburgersPage : ContentPage
    {
        public HamburgersPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //Let's "null" our selected item here
            ((ListView)sender).SelectedItem = null;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var hamburger = ((ListView)sender).SelectedItem as Hamburger;
            if(hamburger == null)
            {
                return;
            }

            await DisplayAlert("Selected Hamburger", hamburger.Name, "OK");
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var hamburger = ((MenuItem)sender).BindingContext as Hamburger;
            if(hamburger == null)
            {
                return;
            }

            await DisplayAlert("Favorited Hamburger", hamburger.Name, "OK");
        }
    }
}
