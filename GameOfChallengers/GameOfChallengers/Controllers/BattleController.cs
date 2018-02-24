using GameOfChallengers.Models;
using GameOfChallengers.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class BattleController
    {
        MonstersListViewModel CurrMonsters;
        public List<Creature> TurnOrder = new List<Creature>();
        public Creature[,] GameBoard = new Creature[3, 6];


        public BattleController(TeamViewModel team)
        {
            GetMonsters();
            GetTurnOrder(team);


        }

        public void GetMonsters()
        {

        }

        public List<Creature> GetTurnOrder(TeamViewModel team)
        {
            //this will get the list that will be cycled through for this round to choose whose turn it is
            //the speed will be found by adding the Speed data from the Creature with all of the boosts from any items (GetBaseSpeed())
            //ties in speed will be broken it the following way:
            //highest level -> highest xp -> character before monster -> alphabetic by name -> first in list order
            List<Creature> turnOrder = null;
            return turnOrder;
        }

        public void AssignItems()
        {

        }

        public void Battle(TeamViewModel team)
        {
            //this will run the turns (using the turn controller) in a loop until either all the team is dead or all the monsters are
        }

        public void AutoBattle(TeamViewModel team)
        {
            //without asking the player for input
            //this will run the turns in a loop until either all the team is dead or all the monsters are
            
            while (true) //CurrMonsters.Instance.Dataset.Count() > 0)
            {
                for(int i=0; i<TurnOrder.Count; i++)//don't forget about if one dies, turnorder[i] == null
                {
                    TurnController turn = new TurnController();
                    if (TurnOrder[i].Type == 0)
                    {
                        int loc = GetNewLoc(TurnOrder[i], GameBoard);
                        GameBoard = turn.Move(TurnOrder[i], loc, GameBoard);
                        Creature target = turn.AutoTarget(1);//get a monster target for the character

                    }
                    else
                    {
                        int loc = GetNewLoc(TurnOrder[i], GameBoard);
                        GameBoard = turn.Move(TurnOrder[i], loc, GameBoard);
                        Creature target = turn.AutoTarget(0);//get a character target for the monster
                    }
                }
            }
            
        }

        public int GetNewLoc(Creature creature, Creature[,] gameBoard)
        {
            //find the creature move it one place(or more) closer to the first monster/character found
            int newloc = 0;
            if(creature.Type == 0)
            {
                

            }
            else
            {

            }
            return newloc;
        }

        //this will return the closest enemy creature to the creature passed in
        private int GetClosest(Creature creature, Creature[,] gameBoard)
        {
            int currIndex;
            for (int i = 0; i < gameBoard.Length; i++)
            {
                //for (int j=0; j<gameBoard[gameBoard.Length].Length; j++)
                //{
                //    if (gameBoard[i][j].Id == creature.Id)
                //    {
                //        currIndex = i;
                //    }
                //}
            }
            return 1;
        }
    }
}
