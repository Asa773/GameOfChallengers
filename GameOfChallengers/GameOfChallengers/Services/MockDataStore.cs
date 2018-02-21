﻿using System;
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
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Name = "Boots", Value = 3, Range = 0, Att = 0, Loc = 0},
                new Item { Id = Guid.NewGuid().ToString(), Name = "Ring", Value = 3, Range = 0, Att = 0, Loc = 0},
            };

            foreach (var data in mockItems)
            {
                _itemDataset.Add(data);
            }

            var mockCreature = new List<Creature>
            {
                //characters
                new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "First Character", Level = 1, Attack = 10, Defense = 10, Speed = 1, XP = 100, MaxHealth = 10, CurrHealth = 5, Alive = true, Loc = 1/*, CInventory = null*/ },
                new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "Second Character", Level = 2, Attack = 20, Defense = 20, Speed = 2, XP = 200, MaxHealth = 20, CurrHealth = 10, Alive = true, Loc = 2/*, CInventory = null*/ },
                new Creature { Id = Guid.NewGuid().ToString(), Type = 0, Name = "Third Character", Level = 3, Attack = 30, Defense = 30, Speed = 3, XP = 300, MaxHealth = 30, CurrHealth = 15, Alive = true, Loc = 3/*, CInventory = null*/ },
                
                
                //monsters
                new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "First Monster", Level = 1, Attack = 10, Defense = 10, Speed = 1, XP = 100, MaxHealth = 10, CurrHealth = 5, Alive = true, Loc = 1/*, CInventory = null*/ },
                new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "Second Monster", Level = 2, Attack = 20, Defense = 20, Speed = 2, XP = 200, MaxHealth = 20, CurrHealth = 10, Alive = true, Loc = 2/*, CInventory = null*/ },
                new Creature { Id = Guid.NewGuid().ToString(), Type = 1, Name = "Third Monster", Level = 3, Attack = 30, Defense = 30, Speed = 3, XP = 300, MaxHealth = 30, CurrHealth = 15, Alive = true, Loc = 3/*, CInventory = null*/ },
                
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