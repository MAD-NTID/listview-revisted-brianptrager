using MvvmHelpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HamburgerApp.Models
{
    [Table("Hamburgers")]
    public class Hamburger : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string RestuarantName { get; set; }
        public string Image { get; set; }
    }
}
