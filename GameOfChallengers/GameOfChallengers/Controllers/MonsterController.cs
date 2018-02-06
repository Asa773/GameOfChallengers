using GameOfChallengers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class MonsterController
    {
        public List<Item> DropItems(Creature monster)
        {
            //drop a random number of items when dead
            List<Item> Dropped = null;
            return Dropped;
        }

        public int GetBaseAttack(Creature monster)
        {
            int baseAttack = 0;//this will be based on the monster stats + any item boosts
            return baseAttack;
        }

        public int GetBaseDamage(Creature monster)
        {
            int baseDamage = 0;//this will be based on the monster stats including item boosts
            return baseDamage;
        }

        public int GetBaseSpeed(Creature monster)
        {
            int baseSpeed = 0;//this will be based on the monster stats + any item boosts
            return baseSpeed;
        }

        public int GetBaseDefense(Creature monster)
        {
            int baseDefense = 0;//this will be based on the monster stats including item boosts
            return baseDefense;
        }

        public int GiveXP(Creature monster, int damageGiven)
        {
            //this will calculate and return the amount of XP to be transferred on a hit
            int XPToGive = 0;
            return XPToGive;
        }

        public void TakeDamage(Creature monster, int amount)
        {
            //monster takes damage and checks for death
        }
    }
}
