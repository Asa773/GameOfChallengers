﻿using GameOfChallengers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class TurnController
    {
        public void Move(Creature creature, int loc)
        {
            bool canMove = MoveTest(creature, loc);
            //move the character if they are allowed to move there(loc)
        }

        private bool MoveTest(Creature creature, int loc)
        {
            //test if a character can move to the selected sqare on the board based on speed
            return true;
        }

        public bool Attack(Creature creature1, Creature creature2)
        {
            //run the attack of creature1 on creature2, return if it succeeded or not
            //creature1 score = roll(1-20) + level + (base attack + attack boosts)
            //creature2 score = (base defense + defense boosts) + level
            //attack is successful(true) if creature1 score > creature2 score
            return true;
        }

        public int DoDamage(Creature creature, int baseDamage)
        {
            //get how much damage the character/monster will do, use getBaseDamage(creature) and a random value
            //final damage done will be weapon damage + 1/4 level, baseDamage, + a roll(1-20)
            int finalDamage = 0;
            return finalDamage;
        }

        public void AutoTarget()
        {

        }
    }
}
