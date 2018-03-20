using GameOfChallengers.Models;
using GameOfChallengers.Services;
using GameOfChallengers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class MonsterController
    {
        public List<Item> DropItems(Creature monster)
        {
            //drop a random number of items when dead
            List<Item> Dropped = new List<Item>();
            int dateSeed = DateTime.Now.Millisecond;
            Random rand = new Random(dateSeed);
            int numOfItems = rand.Next(4);//drop 0, 1, 2, or 3(all) of its items
            List<string> itemIds = monster.GetItemIDs();


            var items = ItemsViewModel.Instance.Dataset;
            for (int i=0; i<numOfItems; i++)
            {

                // Mike, commented out this because if the items are not assigned, then it crashes, because you are not checking if the item exists or not.  so do a check, then access...

                //var item = items.Where(a => a.Id == itemIds[i]).FirstOrDefault();
                //Dropped.Add(item);//Monsters will dropped items will be added to the list
            }

            int chance = rand.Next(10);//Checks for dropping the Unique Item
            if (chance == 1)
            {
                var item = items.Where(a => a.Id == monster.UniqueItem).FirstOrDefault();
                Dropped.Add(item);
            }
            return Dropped;
        }

        public int GetBaseAttack(Creature monster)
        {
            List<string> itemIds = monster.GetItemIDs();
            int baseAttack = 0;//this will be based on the monster stats + any item boosts
            baseAttack += monster.Attack;
            for (int i = 0; i < itemIds.Count; i++)
            {
                var items = ItemsViewModel.Instance.Dataset; //gets the Items
                var item = items.Where(a => a.Id == itemIds[i]).FirstOrDefault();
                if (item.Attribute == AttributeEnum.Attack)
                {
                    baseAttack += item.Value;//Item's value will be increased depending upon the attack
                }
            }
            return baseAttack;
        }

        public int GetBaseDamage(Creature monster)
        {
            int baseDamage = 0;//this will be based on the weapon stats
            Item mitem = monster.GetItemByLocation(ItemLocationEnum.PrimaryHand);
            if (mitem == null)
            {
                return baseDamage;
            }
            var items = ItemsViewModel.Instance.Dataset;//Gets the Items
            var item = items.Where(a => a.Id == mitem.Id).FirstOrDefault();
            int dateSeed = DateTime.Now.Millisecond;
            Random roll = new Random(dateSeed);
            baseDamage += roll.Next(1, item.Damage + 1);//Item's value will be calculated depending upon the damage

            return baseDamage;
        }

        public int GetBaseSpeed(Creature monster)
        {
            List<string> itemIds = monster.GetItemIDs();
            int baseSpeed = 0;//this will be based on the monster stats + any item boosts
            baseSpeed += monster.Speed;
            for (int i = 0; i < itemIds.Count; i++)
            {
                var items = ItemsViewModel.Instance.Dataset;//gets the Items
                var item = items.Where(a => a.Id == itemIds[i]).FirstOrDefault();
                if (item.Attribute == AttributeEnum.Speed)
                {
                    baseSpeed += item.Value;//Item's value will be increased depending upon the speed
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
                var items = ItemsViewModel.Instance.Dataset;//Gets the Items
                var item = items.Where(a => a.Id == itemIds[i]).FirstOrDefault();
                if (item.Attribute == AttributeEnum.Defense)
                {
                    baseDefense += item.Value;//Item's value will be increased depending upon the defense
                }
            }
            return baseDefense;
        }

        public int GiveXP(Creature monster, int damageGiven)
        {
            //this will calculate and return the amount of XP to be transferred on a hit and -= that much from the monster
            double percentToGive = ((double)damageGiven / (double)monster.CurrHealth); //Xp is calculated depending ont he damage and current health
            if (percentToGive > 1.0)
            {
                percentToGive = 1.0;
            }
            int XPToGive = (int)(monster.XP * percentToGive);//XP is calculated
            monster.XP -= XPToGive;
            return XPToGive;
        }

        public bool TakeDamage(Creature monster, int amount)
        {
            //monster takes damage and checks for death
            monster.CurrHealth -= amount;
            if (monster.CurrHealth <= 0)//declares monster's dead when the current health is less than or equal to zero
            {
                monster.Alive = false;
                monster.CurrHealth = 0;//sets the current health to zero when the monster is declared dead inorder to avoid negative values
            }
            return monster.Alive;
        }



    }
}
