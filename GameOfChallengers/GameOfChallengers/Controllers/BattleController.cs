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
        public Creature[,] GameBoard = new Creature[2, 5];


        public BattleController(TeamViewModel team, int round)
        {
            CurrMonsters = new MonstersListViewModel(round);
            GetTurnOrder(team);
            InitializeGameBoard(team, CurrMonsters);

        }

        public List<Creature> GetTurnOrder(TeamViewModel team)
        {
            List<Creature> turnOrder = new List<Creature>();
            // int count = team.Dataset.Count;
            for (int i = 0; i < team.Dataset.Count; i++)
            {
                turnOrder.Add(team.Dataset[i]);
            }
            for (int i = 0; i < CurrMonsters.Dataset.Count; i++)
            {
                turnOrder.Add(CurrMonsters.Dataset[i]);
            }


            CompareByAllCriteria listComparer = new CompareByAllCriteria();
            turnOrder.Sort(listComparer);
           
            //this will get the list that will be cycled through for this round to choose whose turn it is
            //the speed will be found by adding the Speed data from the Creature with all of the boosts from any items (GetBaseSpeed())
            //ties in speed will be broken it the following way:
            //highest level -> highest xp -> character before monster -> alphabetic by name -> first in list order
            return turnOrder;

        }

        public void AssignItems()
        {

        }

        public void Battle(TeamViewModel team)
        {
            //this will run the turns (using the turn controller) in a loop until either all the team is dead or all the monsters are
        }

        public void InitializeGameBoard(TeamViewModel team, MonstersListViewModel CurrMonsters)
        {
            GameBoard[0, 0] = team.Dataset[5];
            GameBoard[1, 0] = team.Dataset[4];
            GameBoard[2, 0] = team.Dataset[3];
            GameBoard[1, 1] = team.Dataset[2];
            GameBoard[0, 2] = team.Dataset[1];
            GameBoard[2, 2] = team.Dataset[0];
            GameBoard[0, 3] = CurrMonsters.Dataset[0];
            GameBoard[1, 3] = CurrMonsters.Dataset[1];
            GameBoard[2, 3] = CurrMonsters.Dataset[2];
            GameBoard[0, 4] = CurrMonsters.Dataset[3];
            GameBoard[1, 5] = CurrMonsters.Dataset[4];
            GameBoard[2, 5] = CurrMonsters.Dataset[5];

        }
        public void AutoBattle(TeamViewModel team)
        {
            //without asking the player for input
            //this will run the turns in a loop until either all the team is dead or all the monsters are

            while (true) //CurrMonsters.Instance.Dataset.Count() > 0)
            {
                for (int i = 0; i < TurnOrder.Count; i++)//don't forget about if one dies, turnorder[i] == null
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

        //Check if this is required
        public int GetNewLoc(Creature creature,Creature[,] GameBoard)
        {
            //find the creature move it one place(or more) closer to the first monster/character found
            int newloc = 0;
            if (creature.Type == 0)
            {


            }
            else
            {

            }
            return newloc;
        }

        //this will return the closest enemy creature to the creature passed in
        private CreatureLocInfo GetClosest(Creature creature)
        {
            CreatureLocInfo info = new CreatureLocInfo();
            CreatureLocInfo EnemyInfo = null;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (GameBoard[i, j].Id == creature.Id)
                    {
                        info.ID = creature.Id;
                        info.row = i;
                        info.col = j;
                        info.type = creature.Type;
                    }
                }
            }
            EnemyInfo = GetEnemy(info);
            return EnemyInfo;
        }

        private CreatureLocInfo GetEnemy(CreatureLocInfo info)
        {
            //enemy away distance is defined as: how many column away + how many row away
            CreatureLocInfo EnemyInfo = new CreatureLocInfo();
            int distance = 0,minDist = 50;
            // List<CreatureLocInfo> distance = new List<CreatureLocInfo>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if(((info.type)== (1- info.type)) && GameBoard[i,j].Id != null)
                    {
                        distance = (Math.Abs(info.row - i) + (Math.Abs(info.col - j)));
                        if (minDist >= distance)
                        {
                            minDist = distance;
                            EnemyInfo.ID = GameBoard[i,j].Id;
                            EnemyInfo.type = GameBoard[i, j].Type;
                            EnemyInfo.col = j;
                            EnemyInfo.row = i;
                        }
                    }
                }

            }

            return EnemyInfo;
        }


    }

    public class CreatureLocInfo 
    {
        public string ID;
        public int row;
        public int col;
        public int type;
    }

    public class CompareByAllCriteria : IComparer<Creature>
    {
        public int Compare(Creature x, Creature y)
        {
            int comparison = x.Speed.CompareTo(y.Speed);
            if (comparison != 0)
            {
                return comparison;
            }

            comparison = x.Level.CompareTo(y.Level);
            if (comparison != 0)
            {
                return comparison;
            }

            comparison = x.XP.CompareTo(y.XP);
            if (comparison != 0)
            {
                return comparison;
            }

            if (x.Type < y.Type)
            {
                return 1;
            }
            else
            {
                if (x.Type > y.Type)
                    return -1;
            }

            comparison = string.Compare(x.Name, y.Name, StringComparison.Ordinal);
            if (comparison != 0)
            {
                return comparison;
            }
            return 0;
        }
    }

    public class CompareByLevel : IComparer<Creature>
    {
        public int Compare(Creature x, Creature y) => x.Level.CompareTo(y.Level);
    }

    public class CompareByXP : IComparer<Creature>
    {
        public int Compare(Creature x, Creature y) => x.XP.CompareTo(y.XP);
    }

    public class CompareByName : IComparer<Creature>
    {
        public int Compare(Creature x, Creature y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal);
    }
}
