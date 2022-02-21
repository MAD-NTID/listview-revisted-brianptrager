using HamburgerApp.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HamburgerApp.ViewModels
{
    public class HamburgerViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Hamburger> Hamburgers { get; set; }
        public ObservableRangeCollection<Grouping<string, Hamburger>> HamburgerGroups { get; set; }
        
        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand<Hamburger> FavoriteCommand { get; set; }

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
                        Application.Current.MainPage.DisplayAlert("Selected Hamburger", selectedHamburger.Name, "OK");
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

            RefreshCommand = new AsyncCommand(OnRefresh);
            FavoriteCommand = new AsyncCommand<Hamburger>(Favorite);

            LoadData();

            //Hamburgers.Add(new Hamburger() { Name = "Butter Burger", RestuarantName = "Culver's", Image = "hamburger.png" });
        }

        async Task OnRefresh()
        {
            IsBusy = true;
            LoadData();
            await Task.Delay(2000);

            IsBusy = false;
        }

        async Task Favorite(Hamburger hamburger)
        {
            if (hamburger == null)
                return;

            await Application.Current.MainPage.DisplayAlert("Selected Hamburger", hamburger.Name, "OK");

        }

        private void LoadData()
        {
            var image = "hamburger.png";
            var tmpList = new List<Hamburger>()
            {
                new Hamburger(){Name="Quarter Pounder", RestuarantName="McDonald's", Image=image},
                new Hamburger(){Name="Big Mac", RestuarantName="McDonald's", Image=image},
                new Hamburger(){Name="Whopper", RestuarantName="Burger King", Image=image},
                new Hamburger(){Name="Whopper Jr.", RestuarantName="Burger King", Image=image},
                new Hamburger(){Name="Stacker King", RestuarantName="Burger King", Image=image},
                new Hamburger(){Name="Little Cheeseburger", RestuarantName="Five Guys", Image=image},
                new Hamburger() { Name = "Butter Burger", RestuarantName = "Culver's", Image = image }
            };

            Hamburgers.AddRange(tmpList);
            //System.Threading.Thread.Sleep(500);

            HamburgerGroups.Add(new Grouping<string, Hamburger>("McDonald's", new[] { Hamburgers[0], Hamburgers[1] }));
            HamburgerGroups.Add(new Grouping<string, Hamburger>("Burger King", new[] { Hamburgers[2], Hamburgers[3], Hamburgers[4] }));
        }
    }
}
