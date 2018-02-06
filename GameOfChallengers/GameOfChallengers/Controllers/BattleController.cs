using GameOfChallengers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class BattleController
    {
        int round = 0;//this will be used for scoring and to determine the level of the 6 monsters
        int turns = 0;//this will count the # of turns for the end game score
        bool auto = false;//this is so that the score can show if the game was turn based or on auto mode
        List<Creature> TotalMonstersKilled = null;//this is so the score can show how many monster were killed
        List<Item> TotalItemsDropped = null;//this is so the score can show all of the items that were dropped
        int TotalXP = 0;//this is to show the total xp gained by all characters on the score page
        Creature[] Gamboard;//this is the gameboard

        //there will be one controller per type and the specific creature will be passed in to the controller methods
        CharacterController Character = new CharacterController();
        MonsterController Monster = new MonsterController();

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
            //the speed will be found by adding the Speed data from the Creature with all of the boosts from any items (GetBaseSpeed())
            //ties in speed will be broken it the following way:
            //highest level -> highest xp -> character before monster -> alphabetic by name -> first in list order
            List<Creature> turnOrder = null;
            return turnOrder;
        }

        public string[] ReportScore(List<Creature> team)
        {
            //the final score will be total XP + # of turns + # of monsters killed
            //this method will report the final score as well as the "Battle History" metadata
            //the metadata is the variables at the top of the page as well as the characters' stats
            string[] Score = null;
            return Score;
        }

        public void Battle(List<Creature> team, List<Creature> monsters, List<Creature> turnOrder)
        {
            //this will run the turns in a loop until either all the team is dead or all the monsters are
        }

        public void AutoBattle(List<Creature> team, List<Creature> monsters, List<Creature> turnOrder)
        {
            auto = true;
            //without asking the player for input
            //this will run the turns in a loop until either all the team is dead or all the monsters are
        }
    }
}
