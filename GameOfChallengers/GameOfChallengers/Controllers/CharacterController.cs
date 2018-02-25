﻿using GameOfChallengers.Models;
using GameOfChallengers.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class CharacterController
    {

        public void EquipItem(Creature character, int itemLoc, Item item)//change to string itemID?
        {
            //equip items at the end of the round to a body location
        }

        public List<Item> DropItems(Creature character)
        {
            //drop all items when dead
            List<Item> Dropped = null;
            List<string> itemIds = character.GetItemIDs();
            for (int i = 0; i < itemIds.Count; i++)
            {
                Dropped.Add(SQLDataStore.Instance.GetAsync_Item(itemIds[i]).Result);//*** Check this, not 100% sure what it is actually doing ***
            }
            return Dropped;
        }

        public int GetBaseAttack(Creature character)
        {
            List<string> itemIds = character.GetItemIDs();
            int baseAttack = 0;//this will be based on the character stats + any item boosts
            baseAttack += character.Attack;
            for (int i = 0; i < itemIds.Count; i++)
            {
                Item item = SQLDataStore.Instance.GetAsync_Item(itemIds[i]).Result;
                if (item.Att == Attributes.Attack)
                {
                    baseAttack += item.Value;
                }
            }
            return baseAttack;
        }

        public int GetBaseDamage(Creature character)
        {

            int baseDamage = 0;//this will be based on the character stats including item boosts

            //How does damage work?

            return baseDamage;
        }

        public int GetBaseSpeed(Creature character)
        {
            List<string> itemIds = character.GetItemIDs();
            int baseSpeed = 0;//this will be based on the character stats + any item boosts
            baseSpeed += character.Speed;
            for (int i = 0; i < itemIds.Count; i++)
            {
                Item item = SQLDataStore.Instance.GetAsync_Item(itemIds[i]).Result;
                if (item.Att == Attributes.Speed)
                {
                    baseSpeed += item.Value;
                }
            }
            return baseSpeed;
        }

        public int GetBaseDefense(Creature character)
        {
            List<string> itemIds = character.GetItemIDs();
            int baseDefense = 0;//this will be based on the character stats including item boosts
            baseDefense += character.Defense;
            for (int i = 0; i < itemIds.Count; i++)
            {
                Item item = SQLDataStore.Instance.GetAsync_Item(itemIds[i]).Result;
                if (item.Att == Attributes.Defence)
                {
                    baseDefense += item.Value;
                }
            }
            return baseDefense;
        }

        public void TestForLevelUp(Creature character, int xp)
        {
            //if character can be leveled up call private helper
            //this method will test the character's xp against a data table with the xp numbers stored
        }

        private void LevelUp(Creature character, int newLevel)
        {
            //level up the character
            //TestForLevelUp will call this method with a new level to change the character to
            //the characters stats will be reset based on a data table with the base stats stored
        }

        public void TakeDamage(Creature character, int amount)
        {
            //character takes damage and checks for death
            character.CurrHealth -= amount;
            if (character.CurrHealth <= 0)
            {
                character.Alive = false;
            }

            //need to do more
            //return bool monster.Alive ?
        }

        
    }
}
