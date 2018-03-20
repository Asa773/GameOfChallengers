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
            //Items
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Name = "Trident", Description = "sharp and pointy X3", Damage = 3, Value = 3, Range = 0, Attribute = AttributeEnum.Attack, Location = ItemLocationEnum.PrimaryHand, ImageURI = "trident.jpeg" });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Name = "Shield", Description = "to block things", Value = 5, Range = 0, Attribute = AttributeEnum.Defense, Location = ItemLocationEnum.OffHand, ImageURI = "sheild.jpg" });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Name = "Running Boots", Description = "boots for running", Value = 1, Range = 0, Attribute = AttributeEnum.Speed, Location = ItemLocationEnum.Feet, ImageURI = "boots.jpg" });



            //characters
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "Golden Fighter ", OnTeam = true, Attack = 10, Defense = 10, Speed = 1, MaxHealth = 15, CurrHealth = 10, Alive = true, ImageURI = "goldenfighter.jpg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "Red Dragon", OnTeam = true, Attack = 20, Defense = 20, Speed = 2, MaxHealth = 25, CurrHealth = 20, Alive = true, ImageURI = "reddragon.jpeg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "Knight", OnTeam = true, Attack = 30, Defense = 30, Speed = 3, MaxHealth = 35, CurrHealth = 30, Alive = true, ImageURI = "knight.jpeg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "Kingsmen", OnTeam = false, Attack = 40, Defense = 40, Speed = 1, MaxHealth = 45, CurrHealth = 40, Alive = true, ImageURI = "kingsmen.jpg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "Warrior", OnTeam = false, Attack = 50, Defense = 50, Speed = 2, MaxHealth = 55, CurrHealth = 50, Alive = true, ImageURI = "warrior.jpeg" });

            //monsters
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "Ugle Face", Alive = true, ImageURI = "uglyface.jpeg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "Mask Man", Alive = true, ImageURI = "maskman.jpeg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "Skeleton", Alive = true, ImageURI = "skeleton.jpeg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "Hitler", Alive = true, ImageURI = "hitler.jpeg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "Zombie", Alive = true, ImageURI = "zombie.jpeg" });
            await AddAsync_Creature(new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "Blue Monster", Alive = true, ImageURI = "bluemonster.jpeg" });



        }

        // Item CRUDi
        public async Task<bool> InsertUpdateAsync_Item(Item data) //Items from JSON
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
            var result = await App.Database.InsertAsync(data); //Add the item
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var result = await App.Database.UpdateAsync(data);//Update the item 
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var result = await App.Database.DeleteAsync(data);//Delete the Item
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Item> GetAsync_Item(string id)
        {
            if (string.IsNullOrEmpty(id))//Read Data
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
            try
            {
                var result = App.Database.Table<Item>().ToListAsync().GetAwaiter().GetResult();
                return result;
            }
            catch (Exception)
            {
                var a = 1;
                return null;
            }
        }

        // Creature CRUDi
        public async Task<bool> AddAsync_Creature(Creature data)
        {
            var result = await App.Database.InsertAsync(data);//Add the creature
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Creature(Creature data)
        {
            var result = await App.Database.UpdateAsync(data);//Update the creature 
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Creature(Creature data)
        {
            var result = await App.Database.DeleteAsync(data);//Delete the creature
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Creature> GetAsync_Creature(string id)
        {
            var result = await App.Database.GetAsync<Creature>(id);//Read Data
            return result;
        }

        public async Task<IEnumerable<Creature>> GetAllAsync_Creature(bool forceRefresh = false)
        {
            try
            {
                var result = App.Database.Table<Creature>().ToListAsync().GetAwaiter().GetResult();
                return result;
            }
            catch (Exception)
            {
                var a = 1;
                return null;
            }
        }

        // Score CRUDi
        public async Task<bool> AddAsync_Score(Score data)
        {
            var result = await App.Database.InsertAsync(data);//Add the score 
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Score(Score data)
        {
            var result = await App.Database.UpdateAsync(data);//Update the score 
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Score(Score data)
        {
            var result = await App.Database.DeleteAsync(data);//Delete the score
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Score> GetAsync_Score(string id)
        {
            var result = await App.Database.GetAsync<Score>(id);//Read Data
            return result;
        }

        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Score>().ToListAsync();
            return result;

        }

    }
}