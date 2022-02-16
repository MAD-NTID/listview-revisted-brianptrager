using HamburgerApp.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HamburgerApp.ViewModels
{
    public class HamburgerViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Hamburger> Hamburgers { get; set; }

        public HamburgerViewModel()
        {
            Title = "Hamburgers";

            Hamburgers = new ObservableRangeCollection<Hamburger>();

            LoadData();

            //Hamburgers.Add(new Hamburger() { Name = "Butter Burger", RestuarantName = "Culver's", Image = "hamburger.png" });
        }

        private void LoadData()
        {
            var image = "hamburger.png";
            var tmpList = new List<Hamburger>()
            {
                new Hamburger(){Name="Quarter Pounder", RestuarantName="McDonald's", Image=image},
                new Hamburger(){Name="Whopper", RestuarantName="Burger King", Image=image},
                new Hamburger(){Name="Little Cheeseburger", RestuarantName="Five Guys", Image=image},
                new Hamburger() { Name = "Butter Burger", RestuarantName = "Culver's", Image = image }
            };

            Hamburgers.AddRange(tmpList);
        }
    }
}
