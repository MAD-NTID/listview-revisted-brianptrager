using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using HamburgerApp.Models;
using SQLite;
using Xamarin.Essentials;

namespace HamburgerApp.Services
{
    public static class HamburgerService
    {
        static SQLiteAsyncConnection db;

        //Initialize the service
        public static async Task Init()
        {
            if (db != null)
                return;

            //Define the path to the database
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "HamburgerDB.db");
            //Create connection to DB
            db = new SQLiteAsyncConnection(dbPath);
            //Create a table
            await db.CreateTableAsync<Hamburger>();
        }

        public static async Task AddHamburger(string name, string restuarantName)
        {
            await Init();
            //Promise to discuss about image on Monday
            var image = "https://people.rit.edu/bptnbs/hamburger.png";

            var hamburger = new Hamburger()
            {
                Name = name,
                RestuarantName = restuarantName,
                Image = image
            };

            var id = await db.InsertAsync(hamburger);
        }

        public static async Task AddHamburger(Hamburger h)
        {
            await Init();

            var id = await db.InsertAsync(h);
        }

        public static async Task RemoveHamburger(int id)
        {
            //Do a quick check if db is connected or not
            await Init();
            //Remove hamburger from DB
            await db.DeleteAsync<Hamburger>(id);
        }

        public static async Task<IEnumerable<Hamburger>> GetHamburgers()
        {
            await Init();

            var hamburgers = await db.Table<Hamburger>().ToListAsync();
            return hamburgers;
        }

        public static async Task<Hamburger> GetHamburger(int id)
        {
            await Init();
            var hamburger = await db.Table<Hamburger>().
                FirstOrDefaultAsync(h => h.Id == id);

            return hamburger;
        }

        //More??
    }
}
