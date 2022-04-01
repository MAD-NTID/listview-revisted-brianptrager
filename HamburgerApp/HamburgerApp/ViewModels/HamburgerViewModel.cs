using HamburgerApp.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using HamburgerApp.Services;

namespace HamburgerApp.ViewModels
{
    public class HamburgerViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Hamburger> Hamburgers { get; set; }
        public ObservableRangeCollection<Grouping<string, Hamburger>> HamburgerGroups { get; set; }

        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand<Hamburger> FavoriteCommand { get; set; }
        public AsyncCommand LoadMoreCommand { get; set; }
        public AsyncCommand AddCommand { get; set; }
        public AsyncCommand<Hamburger> DeleteCommand { get; set; }
        public AsyncCommand ThanosCommand { set; get; }
        public AsyncCommand<String> NavigateCommand { get; set; }

        Hamburger selectedHamburger;
        Hamburger previouslySelected;
        public Hamburger SelectedHamburger
        {
            get => selectedHamburger;
            set
            {
                if(value != null)
                {
                    if (value != previouslySelected)
                    {
                        selectedHamburger = value;
                        Application.Current.MainPage.DisplayAlert("Selected Hamburger", selectedHamburger.Image, "OK");
                        previouslySelected = value;
                        value = null;
                    }
                }

                selectedHamburger = null;
                OnPropertyChanged();
            }
        }

        public HamburgerViewModel()
        {
            Title = "Hamburgers";

            Hamburgers = new ObservableRangeCollection<Hamburger>();
            HamburgerGroups = new ObservableRangeCollection<Grouping<string, Hamburger>>();

            AddCommand = new AsyncCommand(AddHamburger);
            LoadMoreCommand = new AsyncCommand(LoadMore);
            RefreshCommand = new AsyncCommand(Refresh);
            FavoriteCommand = new AsyncCommand<Hamburger>(Favorite);
            DeleteCommand = new AsyncCommand<Hamburger>(DeleteHamburger);
            ThanosCommand = new AsyncCommand(DeleteAllHamburgers);
            NavigateCommand = new AsyncCommand<String>(NavigateToPage);
            //Hamburgers.Add(new Hamburger() { Name = "Butter Burger", RestuarantName = "Culver's", Image = "hamburger.png" });
        }

        private async Task NavigateToPage(String dest)
        {
            Page page = null;
            switch(dest)
            {
                case "AddHamburgerPage": page = new AddHamburgerPage();
                    break;
                //add other pages here
            }
            await App.Current.MainPage.Navigation.PushAsync(page);
        }

        private async Task DeleteAllHamburgers()
        {
            await HamburgerService.RemoveHamburgers();
            await Refresh();
        }

        private async Task DeleteHamburger(Hamburger hamburger)
        {
            await HamburgerService.RemoveHamburger(hamburger.Id);
            await Refresh();
        }

        private async Task AddHamburger()
        {
            
            //var hamburgerName = await App.Current.MainPage.DisplayPromptAsync
            //    ("Add Hamburger", "Enter the name of the hamburger", "OK", 
            //    "Forget it", "Type hamburger name here");
            //var restuarantName = await App.Current.MainPage.DisplayPromptAsync("Add Hamburger", "Enter the name of the resturant", "OK", "Forget it", "Type restuarant name here");
            //await HamburgerService.AddHamburger(hamburgerName, restuarantName);
            //await Refresh();
        }

        private async Task LoadMore()
        {
            await HamburgerService.AddHamburger("World's Greatest Cheeseburger", "Bill Gray's");
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;

            Hamburgers.Clear();
            
            var hamburgers = await HamburgerService.GetHamburgers();
            Hamburgers.AddRange(hamburgers);

            IsBusy = false;
        }

        async Task Favorite(Hamburger hamburger)
        {
            if (hamburger == null)
                return;

            await Application.Current.MainPage.DisplayAlert("Selected Hamburger", hamburger.Name, "OK");

        }
    }
}
