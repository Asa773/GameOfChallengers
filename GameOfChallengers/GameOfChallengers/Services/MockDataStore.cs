using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDetailsCRUDi.Models;
using Creature = MasterDetailsCRUDi.Models.Creature;

namespace MasterDetailsCRUDi.Services
{
    public sealed class MockDataStore : IDataStore
    {

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static MockDataStore _instance;

        public static MockDataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MockDataStore();
                }
                return _instance;
            }
        }

        private List<Item> _itemDataset = new List<Item>();
        private List<Creature> _creatureDataset = new List<Creature>();
        private List<GameItem> _monsterDataset = new List<GameItem>();
       
        private MockDataStore()
        {
            var mockItems = new List<Item>
            {
                
            };

            foreach (var data in mockItems)
            {
                _itemDataset.Add(data);
            }

            var mockCreature = new List<Creature>
            {
                new Creature { Id = Guid.NewGuid().ToString(),Type = "Character", Name = "First Character", Level = "12" , Attack = "Attack stat",Defense = "defense stat",Speed = "Speed" , XP = "3",MaxHealth = "50",currHealth = "20" ,Alive = "Yes" ,Loc = "1st grid"},
                new Creature { Id = Guid.NewGuid().ToString(),Type = "Character", Name = "First Character", Level = "12" , Attack = "Attack stat",Defense = "defense stat",Speed = "Speed" , XP = "3",MaxHealth = "50",currHealth = "20" ,Alive = "Yes" ,Loc = "1st grid"},
                new Creature { Id = Guid.NewGuid().ToString(),Type = "Character", Name = "First Character", Level = "12" , Attack = "Attack stat",Defense = "defense stat",Speed = "Speed" , XP = "3",MaxHealth = "50",currHealth = "20" ,Alive = "Yes" ,Loc = "1st grid"},
                new Creature { Id = Guid.NewGuid().ToString(),Type = "Character", Name = "First Character", Level = "12" , Attack = "Attack stat",Defense = "defense stat",Speed = "Speed" , XP = "3",MaxHealth = "50",currHealth = "20" ,Alive = "Yes" ,Loc = "1st grid"},
                new Creature { Id = Guid.NewGuid().ToString(),Type = "Character", Name = "First Character", Level = "12" , Attack = "Attack stat",Defense = "defense stat",Speed = "Speed" , XP = "3",MaxHealth = "50",currHealth = "20" ,Alive = "Yes" ,Loc = "1st grid"},
                new Creature { Id = Guid.NewGuid().ToString(),Type = "Character", Name = "First Character", Level = "12" , Attack = "Attack stat",Defense = "defense stat",Speed = "Speed" , XP = "3",MaxHealth = "50",currHealth = "20" ,Alive = "Yes" ,Loc = "1st grid"}
            };

            foreach (var data in mockCreature)
            {
                _creatureDataset.Add(data);
            }

            

            var mockGameItem = new List<GameItem>
            {
               new GameItem {Name = "Ring", Value = "12" , Range = "11" , Att = "attribute" , Loc = "finger"  },
               new GameItem {Name = "Ring", Value = "12" , Range = "11" , Att = "attribute" , Loc = "finger"  },
               new GameItem {Name = "Ring", Value = "12" , Range = "11" , Att = "attribute" , Loc = "finger"  },
               new GameItem {Name = "Ring", Value = "12" , Range = "11" , Att = "attribute" , Loc = "finger"  },
               new GameItem {Name = "Ring", Value = "12" , Range = "11" , Att = "attribute" , Loc = "finger"  }
            };

            foreach (var data in mockGameItem)
            {
                _GameItemDataset.Add(data);
            }

        }

        // Item
        public async Task<bool> AddAsync_Item(Item data)
        {
            _itemDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _itemDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetAsync_Item(string id)
        {
            return await Task.FromResult(_itemDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            return await Task.FromResult(_itemDataset);
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