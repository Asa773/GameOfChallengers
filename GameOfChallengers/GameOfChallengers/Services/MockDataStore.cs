using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameOfChallengers.Models;

namespace GameOfChallengers.Services
{
    public sealed class MockDataStore : IDataStore
    {

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
        private List<Score> _scoreDataset = new List<Score>();
        private MockDataStore()
        {
            InitilizeSeedData();
        }

        public void InitilizeSeedData()
        {
         
        
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Name = "Longbow", Description = "A long bow", Damage = 3, Value = 3, Range = 3, Attribute = AttributeEnum.Attack, Location = ItemLocationEnum.PrimaryHand, ImageURI="Bow.jpg"},
                new Item { Id = Guid.NewGuid().ToString(), Name = "Steel Helmet", Description = "helmet made of steel", Value = 3, Range = 0, Attribute = AttributeEnum.Defense, Location = ItemLocationEnum.Head, ImageURI="helmet.jpg"},
                new Item { Id = Guid.NewGuid().ToString(), Name = "Running Boots", Description = "boots for running", Value = 1, Range = 0, Attribute = AttributeEnum.Speed, Location = ItemLocationEnum.Feet, ImageURI="boots.jpg"},
                new Item { Id = Guid.NewGuid().ToString(), Name = "Gun", Description = "ITs a gun", Damage = 5, Value = 5, Range = 5, Attribute = AttributeEnum.Attack, Location = ItemLocationEnum.PrimaryHand, ImageURI="gun.jpg"},
                new Item { Id = Guid.NewGuid().ToString(), Name = "Sword", Description = "sharp and pointy", Value = 2, Range = 0, Attribute = AttributeEnum.Attack, Location = ItemLocationEnum.OffHand, ImageURI="sword.jpeg"},
                new Item { Id = Guid.NewGuid().ToString(), Name = "Trident", Description = "sharp and pointy X3", Damage = 3, Value = 3, Range = 0, Attribute = AttributeEnum.Attack, Location = ItemLocationEnum.PrimaryHand, ImageURI="trident.jpg"},
                new Item { Id = Guid.NewGuid().ToString(), Name = "Shield", Description = "to block things", Value = 5, Range = 0, Attribute = AttributeEnum.Defense, Location = ItemLocationEnum.OffHand, ImageURI="sheild.jpg"},

            };


            foreach (var data in mockItems)
            {
                _itemDataset.Add(data);
            }

            var mockCreature = new List<Creature>
            {
                //characters
                new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "Golden Fighter ", OnTeam = true, Attack = 10, Defense = 10, Speed = 1, MaxHealth = 15, CurrHealth = 10, Alive = true, ImageURI = "goldenfighter.jpg" },
                new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "Red Dragon", OnTeam = true, Attack = 20, Defense = 20, Speed = 2, MaxHealth = 25, CurrHealth = 20, Alive = true, ImageURI = "reddragon.jpeg" },
                new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "Knight", OnTeam = true, Attack = 30, Defense = 30, Speed = 3, MaxHealth = 35, CurrHealth = 30, Alive = true, ImageURI = "knight.jpeg" },
                new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "Kingsmen", OnTeam = false, Attack = 40, Defense = 40, Speed = 1, MaxHealth = 45, CurrHealth = 40, Alive = true, ImageURI = "kingsmen.jpeg" },
                new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "Warrior", OnTeam = false, Attack = 50, Defense = 50, Speed = 2, MaxHealth = 55, CurrHealth = 50,  Alive = true, ImageURI = "warrior.jpeg" },
                
                //monsters
                new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "Ugly Face", Alive = true, ImageURI = "uglyface.jpeg" },
                new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "Mask man", Alive = true, ImageURI = "maskman.jpeg" },
                new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "Skeleton", Alive = true, ImageURI = "skeleton.jpeg" },
                new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "Hitler", Alive = true, ImageURI = "hitler.jpeg" },
                new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "Zombie", Alive = true, ImageURI = "zombie.jpeg" },
                new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "Blue Monster", Alive = true, ImageURI = "bluemonster.jpeg" },


            };

            foreach (var data in mockCreature)
            {
                _creatureDataset.Add(data);
            }



            var mockScore = new List<Score>
            {
               new Score { Id = Guid.NewGuid().ToString(), Name = "Player 1", Date = DateTime.Now, FinalScore = 100, Auto = false,  Round = 0, TotalXP = 0, Turns = 0/*, TotalMonstersKilled = null, TotalItemsDropped = null*/ },

            };

            foreach (var data in mockScore)
            {
                _scoreDataset.Add(data);
            }

        }

        // Creature
        public async Task<bool> AddAsync_Creature(Creature data)
        {
            _creatureDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Creature(Creature data)
        {
            var myData = _creatureDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Creature(Creature data)
        {
            var myData = _creatureDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _creatureDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Creature> GetAsync_Creature(string id)
        {
            return await Task.FromResult(_creatureDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Creature>> GetAllAsync_Creature(bool forceRefresh = false)
        {
            return await Task.FromResult(_creatureDataset);
        }


        // Item
        public async Task<bool> InsertUpdateAsync_Item(Item data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Item(data.Id);
            if (oldData == null)
            {
                _itemDataset.Add(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Item(data);
            if (UpdateResult)
            {
                await AddAsync_Item(data);
                return true;
            }

            return false;
        }
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


        // Score
        public async Task<bool> AddAsync_Score(Score data)
        {
            _scoreDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Score(Score data)
        {
            var myData = _scoreDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Score(Score data)
        {
            var myData = _scoreDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _scoreDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Score> GetAsync_Score(string id)
        {
            return await Task.FromResult(_scoreDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            return await Task.FromResult(_scoreDataset);
        }

    }
}