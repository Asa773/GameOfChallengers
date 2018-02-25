using GameOfChallengers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class TurnController
    {
        public Creature[,] Move(Creature creature, int loc, Creature[,] gameBoard)
        {
            bool canMove = MoveTest(creature, loc);
            //move the character if they are allowed to move there(loc)
            return gameBoard;
        }

        

        public bool Attack(Creature creature1, Creature creature2)
        {
            
            //run the attack of creature1 on creature2, return if it succeeded or not
            //for (roll =1; roll <= 20; roll++)
            //{
            //    creature1 score = roll + level    
            //}

            //creature1 score = roll(1-20) + level + (base attack + attack boosts)
            //creature2 score = (base defense + defense boosts) + level
            //attack is successful(true) if creature1 score > creature2 score
            return true;
        }

        public int DoDamage(Creature creature, int baseDamage)
        {
            //getBaseDamage(creature)
                //get how much damage the character/monster will do, use getBaseDamage(creature) and a random value
            //final damage done will be weapon damage + 1/4 level, baseDamage, + a roll(1-20)
            int finalDamage = 0;
            return finalDamage;
        }

        public Creature AutoTarget(int side)
        {
            Creature c = new Creature();
            return c;
        }
    }
}
