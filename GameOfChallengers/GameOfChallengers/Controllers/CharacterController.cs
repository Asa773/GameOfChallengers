﻿using GameOfChallengers.Models;
using GameOfChallengers.Services;
using GameOfChallengers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class CharacterController
    {
       //used level up table as list for checking leveling up
        List<LevelingUp> lp = new List<LevelingUp>
        {
            new LevelingUp{XP = 0,     Level = 1,Attack = 1,Defense = 1, Speed = 1},
            new LevelingUp{XP = 300,   Level = 2,Attack = 1,Defense = 2, Speed = 1},
            new LevelingUp{XP = 900,   Level = 3,Attack = 2,Defense = 3, Speed = 1},
            new LevelingUp{XP = 2700,  Level = 4,Attack = 2,Defense = 3, Speed = 1},
            new LevelingUp{XP = 6500,  Level = 5,Attack = 2,Defense = 4, Speed = 2},
            new LevelingUp{XP = 14000, Level = 6,Attack = 3,Defense = 4, Speed = 2},
            new LevelingUp{XP = 23000, Level = 7,Attack = 3,Defense = 5, Speed = 2},
            new LevelingUp{XP = 34000, Level = 8,Attack = 3,Defense = 5, Speed = 2},
            new LevelingUp{XP = 48000, Level = 9,Attack = 3,Defense = 5, Speed = 2},
            new LevelingUp{XP = 64000, Level = 10,Attack = 4,Defense = 6, Speed = 3},
            new LevelingUp{XP = 85000, Level = 11,Attack = 4,Defense = 6, Speed = 3},
            new LevelingUp{XP = 100000,Level = 12,Attack = 4,Defense = 6, Speed = 3},
            new LevelingUp{XP = 120000,Level = 13,Attack = 4,Defense = 7, Speed = 3},
            new LevelingUp{XP = 140000,Level = 14,Attack = 5,Defense = 7, Speed = 3},
            new LevelingUp{XP = 165000,Level = 15,Attack = 5,Defense = 7, Speed = 4},
            new LevelingUp{XP = 195000,Level = 16,Attack = 5,Defense = 8, Speed = 4},
            new LevelingUp{XP = 225000,Level = 17,Attack = 5,Defense = 8, Speed = 4},
            new LevelingUp{XP = 265000,Level = 18,Attack = 6,Defense = 8, Speed = 4},
            new LevelingUp{XP = 305000,Level = 19,Attack = 7,Defense = 9, Speed = 4},
            new LevelingUp{XP = 355000,Level = 20,Attack = 8,Defense = 10, Speed = 5},
        };

        
        public List<Item> DropItems(Creature character)
        {
            //drop all items when dead and adds it to a list
            List<Item> Dropped = new List<Item>();
            List<string> itemIds = character.GetItemIDs();//getting all the items on character
            var items = ItemsViewModel.Instance.Dataset;
            if (itemIds != null)//checking if there are any items
            {
                for (int i = 0; i < itemIds.Count; i++)
                {
                    var item = items.Where(a => a.Id == itemIds[i]).FirstOrDefault();
                    Dropped.Add(item);//adding dropped item to list

                }
            }
            return Dropped;
        }

        public int GetBaseAttack(Creature character)//Calculates the attack done by the character
        {
            List<string> itemIds = character.GetItemIDs();
            int baseAttack = 0;//this will be based on the character stats + any item boosts
            baseAttack += character.Attack;
            for (int i = 0; i < itemIds.Count; i++)
            {
                var items = ItemsViewModel.Instance.Dataset;
                var item = items.Where(a => a.Id == itemIds[i]).FirstOrDefault();
                if (item.Attribute == AttributeEnum.Attack)
                {
                    baseAttack += item.Value;//the item value is increased depending on the attack
                }
            }
            return baseAttack;
        }

        public int GetBaseDamage(Creature character)//Damage done will be calculated
        {
            int baseDamage = 0;//this will be based on the weapon stats
            Item citem = character.GetItemByLocation(ItemLocationEnum.PrimaryHand);
            if(citem == null)
            {
                return baseDamage;
            }
            var items = ItemsViewModel.Instance.Dataset;
            var item = items.Where(a => a.Id == citem.Id).FirstOrDefault();
            int dateSeed = DateTime.Now.Millisecond;
            Random roll = new Random(dateSeed);
            baseDamage += roll.Next(1, item.Damage + 1);//item value will be changed depending on the damage 

            return baseDamage;
        }

        public int GetBaseSpeed(Creature character)
        {
            List<string> itemIds = character.GetItemIDs();
            int baseSpeed = 0;//this will be based on the character stats + any item boosts
            baseSpeed += character.Speed;
            for (int i = 0; i < itemIds.Count; i++)
            {
                var items = ItemsViewModel.Instance.Dataset;
                var item = items.Where(a => a.Id == itemIds[i]).FirstOrDefault();
                if (item.Attribute == AttributeEnum.Speed)
                {
                    baseSpeed += item.Value;//the item value is increased
                }
            }
            return baseSpeed;
        }

        public int GetBaseDefense(Creature character)//Defense is calculated 
        {
            List<string> itemIds = character.GetItemIDs();
            int baseDefense = 0;//this will be based on the character stats including item boosts
            baseDefense += character.Defense;
            for (int i = 0; i < itemIds.Count; i++)
            {
                var items = ItemsViewModel.Instance.Dataset;
                var item = items.Where(a => a.Id == itemIds[i]).FirstOrDefault();
                if (item.Attribute == AttributeEnum.Defense)
                {
                    baseDefense += item.Value;//the item value will be increased
                }
            }
            return baseDefense;
        }

        public bool TestForLevelUp(Creature character, int xp)
        {
            //if character can be leveled up call private helper
            //this method will test the character's xp against a data table with the xp numbers stored
            character.XP += xp;
            bool DidLevelUp = false;
            int NewLevel = 0;
            for (int i = 0; i < lp.Count; i++)
            {
                if(character.XP >= lp[i].XP )
                {
                    NewLevel = lp[i].Level;//Level assigned based on XP
                }
            }
            if (character.Level < NewLevel)
            {
                LevelUp(character, NewLevel);//Increase Level if characters level is less than new level
                DidLevelUp = true;
            }
            return DidLevelUp;
        }

        private void LevelUp(Creature character, int newLevel)
        {
            //level up the character
            //TestForLevelUp will call this method with a new level to change the character to
            //the characters stats will be reset based on a data table with the base stats stored
            int dateSeed = DateTime.Now.Millisecond;
            Random rand = new Random(dateSeed);
            int rolld = rand.Next(11);
            character.Level = newLevel; //gets new level
            character.Attack = lp[newLevel - 1].Attack; //gets attack based on the new level
            character.Defense = lp[newLevel - 1].Defense;//gets defense based on the new level
            character.Speed = lp[newLevel - 1].Speed;//gets speed dependong on the new level
            int offset = character.MaxHealth - character.CurrHealth;
            character.MaxHealth = rolld * newLevel;
            character.CurrHealth = character.MaxHealth - offset;//calculating the current health
            
        }

        public bool TakeDamage(Creature character, int amount)
        {
            //character takes damage and checks for death
            character.CurrHealth -= amount;
            if (character.CurrHealth <= 0)
            {
                character.Alive = false;//when current health is less than or equa to zero it is said to be dead
                character.CurrHealth = 0;//to avoid negative values we set the current health to zero once set to dead
            }
            return character.Alive;
        }
        

    }
}
