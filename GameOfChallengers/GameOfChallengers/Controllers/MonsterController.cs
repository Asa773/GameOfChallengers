using GameOfChallengers.Models;
using GameOfChallengers.Services;
using GameOfChallengers.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class MonsterController
    {
        ItemsViewModel IViewModel = new ItemsViewModel();
        public List<Item> DropItems(Creature monster)
        {
            //drop a random number of items when dead
            List<Item> Dropped = null;
            int dateSeed = DateTime.Now.Millisecond;
            Random rand = new Random(dateSeed);
            int numOfItems = rand.Next(4);//drop 0, 1, 2, or 3(all) of its items
            List<string> itemIds = monster.GetItemIDs();
            for (int i=0; i<numOfItems; i++)
            {
                Dropped.Add(SQLDataStore.Instance.GetAsync_Item(itemIds[i]).Result);//*** Check this, not 100% sure what it is actually doing ***
            }
            return Dropped;
        }
        
        public int GetBaseAttack(Creature monster)
        {
            List<string> itemIds = monster.GetItemIDs();
            int baseAttack = 0;//this will be based on the monster stats + any item boosts
            baseAttack += monster.Attack;
            for(int i=0; i<itemIds.Count; i++)
            {
                Item item = SQLDataStore.Instance.GetAsync_Item(itemIds[i]).Result;
                if(item.Att == Attributes.Attack)
                {
                    baseAttack += item.Value;
                }
            }
            return baseAttack;
        }

        public int GetBaseDamage(Creature monster)
        {
            List<string> itemIds = monster.GetDamageIDs();
            int baseDamage = 0;//this will be based on the weapon stats
            for (int i = 0; i < itemIds.Count; i++)
            {
                Item item = SQLDataStore.Instance.GetAsync_Item(itemIds[i]).Result;
                if (item.Att == Attributes.Attack)
                {
                    baseDamage += item.Value;
                }
            }
            return baseDamage;
        }

        public int GetBaseSpeed(Creature monster)
        {
            List<string> itemIds = monster.GetItemIDs();
            int baseSpeed = 0;//this will be based on the monster stats + any item boosts
            baseSpeed += monster.Speed;
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

        public int GetBaseDefense(Creature monster)
        {
            List<string> itemIds = monster.GetItemIDs();
            int baseDefense = 0;//this will be based on the monster stats including item boosts
            baseDefense += monster.Defense;
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

        public int GiveXP(Creature monster, int damageGiven)
        {
            //this will calculate and return the amount of XP to be transferred on a hit and -= that much from the monster
            double percentToGive = (damageGiven / monster.CurrHealth);
            int XPToGive = (int)(monster.XP * percentToGive);
            monster.XP -= XPToGive;
            return XPToGive;
        }

        public bool TakeDamage(Creature monster, int amount)
        {
            //monster takes damage and checks for death
            monster.CurrHealth -= amount;
            if(monster.CurrHealth <= 0)
            {
                monster.Alive = false;
            }
            return monster.Alive;
        }

        
    }
}
