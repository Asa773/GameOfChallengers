using GameOfChallengers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class BattleController
    {
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

        public void Battle(List<Creature> team, List<Creature> monsters, List<Creature> turnOrder)
        {
            //this will run the turns (using the turn controller) in a loop until either all the team is dead or all the monsters are
        }

        public void AutoBattle(List<Creature> team, List<Creature> monsters, List<Creature> turnOrder)
        {
            //without asking the player for input
            //this will run the turns in a loop until either all the team is dead or all the monsters are
        }
    }
}
