using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Models;

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

        private SQLDataStore()
        {
            App.Database.CreateTableAsync<Item>().Wait();
            App.Database.CreateTableAsync<Creature>().Wait();
            App.Database.CreateTableAsync<Score>().Wait();
        }

        // Create the Database Tables
        private void CreateTables()
        {
            App.Database.CreateTableAsync<Item>().Wait();
            App.Database.CreateTableAsync<Creature>().Wait();
            App.Database.CreateTableAsync<Score>().Wait();
            
        }

        // Delete the Datbase Tables by dropping them
        private void DeleteTables()
        {
            App.Database.DropTableAsync<Item>().Wait();
            App.Database.DropTableAsync<Creature>().Wait();
            App.Database.DropTableAsync<Score>().Wait();
            
        }

        // Tells the View Models to update themselves.
        private void NotifyViewModelsOfDataChange()
        {
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            MonstersViewModel.Instance.SetNeedsRefresh(true);
            CharactersViewModel.Instance.SetNeedsRefresh(true);
            ScoresViewModel.Instance.SetNeedsRefresh(true);
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
        private async void InitilizeSeedData()
        {

            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Name = "Trident", Description = "sharp and pointy X3", Damage = 3, Value = 3, Range = 0, Attribute = AttributeEnum.Attack, Location = ItemLocationEnum.PrimaryHand, ImageURI = "trident.jpeg" });


            /*NEED TO ADD  each item ID set to null on each creature*/
            //characters
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "Fighter 1", OnTeam = true, Attack = 10, Defense = 10, Speed = 1, MaxHealth = 15, CurrHealth = 10, Alive = true, ImageURI = "fighter1.jpg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "Fighter 2", OnTeam = true, Attack = 20, Defense = 20, Speed = 2, MaxHealth = 25, CurrHealth = 20, Alive = true, ImageURI = "fighter2.jpeg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "Fighter 3", OnTeam = true, Attack = 30, Defense = 30, Speed = 3, MaxHealth = 35, CurrHealth = 30, Alive = true, ImageURI = "fighter3.jpeg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "Fighter 4", OnTeam = false, Attack = 40, Defense = 40, Speed = 1, MaxHealth = 45, CurrHealth = 40, Alive = true, ImageURI = "fighter4.jpg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "Fighter 5", OnTeam = false, Attack = 50, Defense = 50, Speed = 2, MaxHealth = 55, CurrHealth = 50, Alive = true, ImageURI = "fighter5.jpeg" });

            //monsters
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "first Monster", Alive = true, ImageURI = "Monster1.jpeg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "second Monster", Alive = true, ImageURI = "monster2.jpeg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "third Monster", Alive = true, ImageURI = "monster3.jpeg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "fourth Monster", Alive = true, ImageURI = "monster4.jpeg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "fifth Monster", Alive = true, ImageURI = "monster5.jpeg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "sixth Monster", Alive = true, ImageURI = "monster6.jpeg" });



        }

        // Item
        public async Task<bool> InsertUpdateAsync_Item(Item data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Item(data.Id);
            if (oldData == null)
            {
                // If it does not exist, add it to the DB
                var InsertResult = await AddAsync_Item(data);
                if (InsertResult)
                {
                    return true;
                }

                return false;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Item(data);
            if (UpdateResult)
            {
                return true;
            }

            return false;
        }
        public async Task<bool> AddAsync_Item(Item data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Item> GetAsync_Item(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            // Need to add a try catch here, to catch when looking for something that does not exist in the db...
            try
            {
                var result = await App.Database.GetAsync<Item>(id);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
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

        // Score
        public async Task<bool> AddAsync_Score(Score data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Score(Score data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Score(Score data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Score> GetAsync_Score(string id)
        {
            var result = await App.Database.GetAsync<Score>(id);
            return result;
        }

        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Score>().ToListAsync();
            return result;

        }

    }
}