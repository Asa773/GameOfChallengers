using GameOfChallengers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class BattleController
    {
        public void Move(Creature creature, int loc)
        {
            bool canMove = MoveTest(creature, loc);
            //move the character if they are allowed to move there(loc)
        }

        private bool MoveTest(Creature creature, int loc)
        {
            //test if a character can move to the selected sqare on the board
            return true;
        }

        public void Attack(Creature creature1, Creature creature2)
        {
            //run the attack of creature1 on creature2
        }

        public int DoDamage(Creature creature, int baseDamage)
        {
            //get how much damage the character/monster will do, use getBaseDamage(creature)
            int finalDamage = 0;
            return finalDamage;
        }
    }
}
