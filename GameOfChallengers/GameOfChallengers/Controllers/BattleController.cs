using GameOfChallengers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class BattleController
    {
        int round = 0;//this will be used for scoring and to determine the level of the 6 monsters
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
            //run the attack of creature1 on creature2, return if it succeeded  or not
            return true;
        }

        public int DoDamage(Creature creature, int baseDamage)
        {
            //get how much damage the character/monster will do, use getBaseDamage(creature) and a random value
            int finalDamage = 0;
            return finalDamage;
        }

        public List<Creature> GetMonsters()
        {
            //this will get the list of six monsters to fight the team
            //the monster type will be random(for each monster) and the level will be determined by the round #
            List<Creature> monsters = null;
            return monsters;
        }

        public List<Creature> GetTurnOrder(List<Creature> team, List<Creature> monsters)
        {
            //this will get the list that will be cycled through for this round to choose whose turn it is
            List<Creature> turnOrder = null;
            return turnOrder;
        }

        //!!! ALERT !!!
        public string ReportScore(string info)
        {
            //we have figure this part out before submission
            return "";
        }

        public void Battle(List<Creature> team, List<Creature> monsters, List<Creature> turnOrder)
        {
            //this will run the turns in a loop until either all the team is dead or all the monsters are
        }

        public void AutoBattle(List<Creature> team, List<Creature> monsters, List<Creature> turnOrder)
        {
            //without asking the player for input
            //this will run the turns in a loop until either all the team is dead or all the monsters are
        }
    }
}
