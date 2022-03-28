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
            RefreshCommand = new AsyncCommand(OnRefresh);
            FavoriteCommand = new AsyncCommand<Hamburger>(Favorite);

            LoadData();

            //Hamburgers.Add(new Hamburger() { Name = "Butter Burger", RestuarantName = "Culver's", Image = "hamburger.png" });
        }

        private async Task AddHamburger()
        {
            await HamburgerService.AddHamburger(new Hamburger() 
            { Name = "James' Burger", RestuarantName = "Grilled Anderson Style", 
                Image= "https://people.rit.edu/bptnbs/hamburger.png" });
        }

        private async Task LoadMore()
        {
            await LoadData();
            await Task.Delay(500);
        }

        async Task OnRefresh()
        {
            IsBusy = true;

            Hamburgers.Clear();
            await LoadData();
            await Task.Delay(2000);

            IsBusy = false;
        }

        async Task Favorite(Hamburger hamburger)
        {
            if (hamburger == null)
                return;

            await Application.Current.MainPage.DisplayAlert("Selected Hamburger", hamburger.Name, "OK");

        }

        private async Task LoadData()
        {
            var image = "https://people.rit.edu/bptnbs/hamburger.png";
            //var tmpList = new List<Hamburger>()
            //{
            //    new Hamburger(){Name="Quarter Pounder", RestuarantName="McDonald's", Image="https://people.rit.edu/bptnbs/hamburger.png"},
            //    new Hamburger(){Name="Big Mac", RestuarantName="McDonald's", Image=image},
            //    new Hamburger(){Name="Whopper", RestuarantName="Burger King", Image=image},
            //    new Hamburger(){Name="Whopper Jr.", RestuarantName="Burger King", Image=image},
            //    new Hamburger(){Name="Stacker King", RestuarantName="Burger King", Image=image},
            //    new Hamburger(){Name="Little Cheeseburger", RestuarantName="Five Guys", Image=image},
            //    new Hamburger() { Name = "Butter Burger", RestuarantName = "Culver's", Image = image }
            //};

            //Hamburgers.Add(new Hamburger()
            //{
            //    Name = "Quarter Pounder",
            //    RestuarantName = "McDonald's",
            //    Image = "https://people.rit.edu/bptnbs/hamburger.png"
            //});

            await HamburgerService.AddHamburger("Quarter Pounder", "McDonald's");
            var hamburgers = await HamburgerService.GetHamburgers();
            Hamburgers.AddRange(hamburgers);
            //Hamburgers.AddRange(tmpList);
            //System.Threading.Thread.Sleep(500);

            //HamburgerGroups.Add(new Grouping<string, Hamburger>("McDonald's", new[] { Hamburgers[0], Hamburgers[1] }));
            //HamburgerGroups.Add(new Grouping<string, Hamburger>("Burger King", new[] { Hamburgers[2], Hamburgers[3], Hamburgers[4] }));
        }
    }
}
