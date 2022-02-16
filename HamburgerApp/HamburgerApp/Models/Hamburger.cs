using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HamburgerApp.Models
{
    public class Hamburger : ObservableObject
    {
        public string Name { get; set; }
        public string RestuarantName { get; set; }
        public string Image { get; set; }
    }
}
