using HamburgerApp.Models;
using HamburgerApp.Services;
using HamburgerApp.ViewModels;
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
            BindingContext = new HamburgerViewModel();
        }
    }
}
