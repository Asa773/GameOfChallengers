using GameOfChallengers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class MonsterController
    {
        public void DropItems(Creature creature)
        {
            //drop a random number of items when dead
        }

        public int GetBaseAttack(Creature creature)
        {
            int baseAttack = 0;//this will be based on the monster stats
            return baseAttack;
        }

        public int GetBaseDamage(Creature creature)
        {
            int baseDamage = 0;//this will be based on the monster stats
            return baseDamage;
        }

        public void TakeDamage(Creature creature, int amount)
        {
            //monster takes damage and checks for death
        }
    }
}
