using GameOfChallengers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class MonsterController
    {
        public void DropItems(Creature monster)
        {
            //drop a random number of items when dead
        }

        public int GetBaseAttack(Creature monster)
        {
            int baseAttack = 0;//this will be based on the monster stats
            return baseAttack;
        }

        public int GetBaseDamage(Creature monster)
        {
            int baseDamage = 0;//this will be based on the monster stats
            return baseDamage;
        }

        public int GiveXP(Creature monster, int damageGiven)
        {
            //this will calculate and return the amount of XP to be transfered on a hit
            int XPToGive = 0;
            return XPToGive;
        }

        public void TakeDamage(Creature monster, int amount)
        {
            //monster takes damage and checks for death
        }
    }
}
