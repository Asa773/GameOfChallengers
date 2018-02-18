using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameOfChallengers.ViewModels;


using GameOfChallengers.Models;

[assembly: Xamarin.Forms.Dependency(typeof(GameOfChallengers.Services.SQLDataStore))]
namespace GameOfChallengers.Services
{
    public sealed class SQLDataStore : IDataStore
    {
        private static SQLDataStore _instance;

        public static SQLDataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SQLDataStore();
                }
                return _instance;
            }
        }

        List<Item> _itemDataset = new List<Item>();
        List<Creature> _creatureDataset = new List<Creature>();
        List<GameItem> _gameitemDataset = new List<GameItem>();

        private SQLDataStore()
        {
            App.Database.CreateTableAsync<Item>().Wait();
            App.Database.CreateTableAsync<Creature>().Wait();
            App.Database.CreateTableAsync<GameItem>().Wait();
        }

        // Create the Database Tables
        private void CreateTables()
        {
            App.Database.CreateTableAsync<Item>().Wait();
            App.Database.CreateTableAsync<Creature>().Wait();
            App.Database.CreateTableAsync<GameItem>().Wait();

        }

        // Delete the Datbase Tables by dropping them
        private void DeleteTables()
        {
            App.Database.DropTableAsync<Item>().Wait();
            App.Database.DropTableAsync<Creature>().Wait();
            App.Database.DropTableAsync<GameItem>().Wait();
            
        }

        // Tells the View Models to update themselves.
        private void NotifyViewModelsOfDataChange()
        {
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            CreatureViewModel.Instance.SetNeedsRefresh(true);
            GameItemViewModel.Instance.SetNeedsRefresh(true);
        }
        public void InitializeDatabaseNewTables()
        {
            // Delete the tables
            DeleteTables();

            // make them again
            CreateTables();

            // Populate them
            InitilizeSeedData();

            // Tell View Models they need to refresh
            NotifyViewModelsOfDataChange();
        }
        private void InitilizeSeedData()
        {

           
            await AddAsync_Item( new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description = "This is an item description." });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description = "This is an item description." });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description = "This is an item description." });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description = "This is an item description." });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description = "This is an item description." });


            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(),Type = "Character", Name = "First Character", Level = "12" , Attack = "Attack stat",Defense = "defense stat",Speed = "Speed" , XP = "3",MaxHealth = "50",currHealth = "20" ,Alive = "Yes" ,Loc = "1st grid",CInventory = "Creature Inventory"});
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = "Character", Name = "First Character", Level = "12", Attack = "Attack stat", Defense = "defense stat", Speed = "Speed", XP = "3", MaxHealth = "50", currHealth = "20", Alive = "Yes", Loc = "1st grid", CInventory = "Creature Inventory" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = "Character", Name = "First Character", Level = "12", Attack = "Attack stat", Defense = "defense stat", Speed = "Speed", XP = "3", MaxHealth = "50", currHealth = "20", Alive = "Yes", Loc = "1st grid", CInventory = "Creature Inventory" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = "Character", Name = "First Character", Level = "12", Attack = "Attack stat", Defense = "defense stat", Speed = "Speed", XP = "3", MaxHealth = "50", currHealth = "20", Alive = "Yes", Loc = "1st grid", CInventory = "Creature Inventory" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = "Character", Name = "First Character", Level = "12", Attack = "Attack stat", Defense = "defense stat", Speed = "Speed", XP = "3", MaxHealth = "50", currHealth = "20", Alive = "Yes", Loc = "1st grid", CInventory = "Creature Inventory" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = "Character", Name = "First Character", Level = "12", Attack = "Attack stat", Defense = "defense stat", Speed = "Speed", XP = "3", MaxHealth = "50", currHealth = "20", Alive = "Yes", Loc = "1st grid", CInventory = "Creature Inventory" });


            await AddAsync_GameItem(new GameItem {Name = "Ring", Value = "12" , Range = "11" , Att = "attribute" , Loc = "finger"  });
            await AddAsync_GameItem(new GameItem { Name = "Ring", Value = "12", Range = "11", Att = "attribute", Loc = "finger" });
            await AddAsync_GameItem(new GameItem { Name = "Ring", Value = "12", Range = "11", Att = "attribute", Loc = "finger" });
            await AddAsync_GameItem(new GameItem { Name = "Ring", Value = "12", Range = "11", Att = "attribute", Loc = "finger" });
            await AddAsync_GameItem(new GameItem { Name = "Ring", Value = "12", Range = "11", Att = "attribute", Loc = "finger" });


        }

        public async Task<bool> AddAsync_Item(Item item)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAsync_Item(Item item)
        {
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync_Item(Item item)
        {

            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<Item> GetAsync_Item(string id)
        {
            var result = await App.Database.GetAsync<Item>(id);
            return result;
        }

        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Item>().ToListAsync();
            return result;
        }

        // Creature
        public async Task<bool> AddAsync_Creature(Creature data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAsync_Creature(Creature data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync_Creature(Creature data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<Creature> GetAsync_Creature(string id)
        {
            var result = await App.Database.GetAsync<Creature>(id);
            return result;
        }

        public async Task<IEnumerable<Creature>> GetAllAsync_Creature(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Creature>().ToListAsync();
            return result;
        }


    


        // GameItem
        public async Task<bool> AddAsync_GameItem(GameItem data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAsync_GameItem(GameItem data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync_GameItem(GameItem data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<Score> GetAsync_GameItem(string id)
        {
            var result = await App.Database.GetAsync<GameItem>(id);
            return result;
        }

        public async Task<IEnumerable<GameItem>> GetAllAsync_GameItem(bool forceRefresh = false)
        {
            var result = await App.Database.Table<GameItem>().ToListAsync();
            return result;
        }

    }
}