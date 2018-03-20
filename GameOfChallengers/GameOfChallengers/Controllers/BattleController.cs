using GameOfChallengers.Models;
using GameOfChallengers.Services;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Views.Battle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace GameOfChallengers.Controllers
{
   public class BattleController
    {
        MonstersListViewModel CurrMonsters;
        TeamViewModel team;
        public List<Creature> TurnOrder = new List<Creature>();
        public List<Item> ItemPool = new List<Item>();//Dropped Items list will be stored
        public List<Creature> originalMonsters = new List<Creature>();
        public Creature[,] GameBoard = new Creature[3, 6];
        //there will be one controller per type and the specific creature will be passed in to the controller methods
        TurnController turn = new TurnController();
        CharacterController CC = new CharacterController();
        MonsterController MC = new MonsterController();
        String img = "BlankImage.jpeg";
        int turns = 0;
        int totalXP = 0;
        public int SelectedGridCellI;
        public int SelectedGridCellJ;
        int turnCounter = 0;//for regular battle to keep track of the turn

        public BattleController()
        {
            CurrMonsters = MonstersListViewModel.Instance;//Monsters team for the Battle
            team = TeamViewModel.Instance;//Characters Team for the Battle
        }

        public void SetBattleController(int round)
        {
            CurrMonsters.setRound(round);//Monsters will reset for every round
            CurrMonsters.setMonsters();
            originalMonsters.RemoveRange(0, originalMonsters.Count);
            foreach(Creature monster in CurrMonsters.Dataset)
            {
                Creature tempMonster = new Creature();
                tempMonster.Update(monster);
                tempMonster.Id = monster.Id;
                originalMonsters.Add(tempMonster);
            }
            TurnOrder = GetTurnOrder();
            InitializeGameBoard();//GameBoard will be initialized
        }

        internal ImageSource GetCreatureTurnImage()
        {
            return img;
        }

        public List<Creature> GetTurnOrder()
        {
            //this will get the list that will be cycled through for this round to choose whose turn it is
            //the speed will be found by adding the Speed data from the Creature with all of the boosts from any items (GetBaseSpeed())
            //ties in speed will be broken it the following way:
            //highest level -> highest xp -> character before monster -> alphabetic by name -> first in list order
            List<Creature> turnOrder = new List<Creature>();
            for (int i = 0; i < TeamViewModel.Instance.Dataset.Count; i++)
            {
                turnOrder.Add(team.Dataset[i]);
            }
            for (int i = 0; i < CurrMonsters.Dataset.Count; i++)
            {
                turnOrder.Add(CurrMonsters.Dataset[i]);
            }


            CompareByAllCriteria listComparer = new CompareByAllCriteria();
            turnOrder.Sort(listComparer);
            return turnOrder;

        }

        public void OneCharacterFights(Score gameScore)
        {
            Creature character = TurnOrder[turnCounter];//It's character's turn
            Creature target = GameBoard[SelectedGridCellI, SelectedGridCellJ];//get a monster target for the character
            if (target == null)
            {
                return;
            }

            CharacterAutoTurn(character, target, gameScore);//the attack of the character on the target(monster) method is called here
        }

        public int MonstersFight(Score gameScore)
        {
            return 0;
        }

        public Score BattleEnd(Score gameScore)
        {
            //this will finish the battle by assigning items as well as loading and returning the score
            AssignItems();//after the battle is finished items are assigned to the characters
            gameScore.Turns += turns;
            gameScore.TotalXP += totalXP;
            string message =
                "Battle Ended" +
                " Total Experience :" + totalXP +
                 " Turns :" + turns
                 ;
            Debug.WriteLine(message);
            return gameScore;
        }

        public Score AutoBattle(Score gameScore)
        {
            //this will run the turns in a loop until either all the team is dead or all the monsters are
           //Characters and Monsters list will be displayed when the battle starts
            string message = "Battle Start" + " Characters :\n";
            foreach(Creature character in team.Dataset)
            {
                message += character.FormatOutput(character) + "\n";
            }
            Debug.WriteLine(message);

            message = "Battle Start" + " Monsters :\n";
            foreach (Creature monster in CurrMonsters.Dataset)
            {
                message += monster.FormatOutput(monster) + "\n";
            }
            Debug.WriteLine(message);
            message = string.Empty;

            while (CurrMonsters.Dataset.Count > 0)
            {
                if(team.Dataset.Count <= 0)
                {
                    break;
                }
                for (int i = 0; i < TurnOrder.Count; i++)
                {

                    
                    turns++;//turns increase for every round
                    message = "New Turn :" + turns;
                    Debug.WriteLine(message);
                    
                    if (TurnOrder[i].Type == 0)//checks if the creature is character or monster
                    {
                        Creature character = TurnOrder[i];//It's character's turn
                        Creature target = AutoTarget(character);//get a monster target for the character
                        if (target == null)
                        {
                            break;
                        }

                        CharacterAutoTurn(character, target, gameScore);//the attack of the character on the target(monster) method is called here
                    }
                    else
                    {
                        Creature monster = TurnOrder[i];//monster's turn
                        Creature target = AutoTarget(monster);//get a character target for the monster
                        if (target == null)
                        {
                            break;
                        }

                        MonsterAutoTurn(monster, target, gameScore);//the attack of the monster on the target(character) method is called here
                    }
                }
            }
            AssignItems();//after the battle is finished items are assigned to the characters
            gameScore.Turns += turns;
            gameScore.TotalXP += totalXP;
            message =
                "Battle Ended" +
                " Total Experience :" + totalXP + 
                 " Turns :" + turns 
                 ;
            Debug.WriteLine(message);

            return gameScore;//finally the score will be displayed
        }

        //this method will be called by when It's character's turn
        public void CharacterAutoTurn(Creature character, Creature target, Score score)
        {
            string message = string.Empty;
            GetNewLoc(character);//will get the character's location
            Move(character);
            if (!CanHit(character, target))//check if the character can hit the target
            {
                return;
            }
            bool hit = turn.Attack(character, target);//character attacks the target
            if (hit)
            {
                message = "Character " + character.Name + " attacks " + target.Name;
                Debug.WriteLine(message);

                int damageToDo = turn.DamageToDo(character);//Damage for the character will be calculated
                int xpToGive = MC.GiveXP(target, damageToDo);//Experience will be calaculated for the character
                totalXP += xpToGive;
                CC.TestForLevelUp(character, xpToGive);//Check the Level up
                bool monsterAlive;//after the attack it will check if the monster is alive or not
                MC.TakeDamage(target, damageToDo);
                monsterAlive = target.Alive;

                if (monsterAlive == false)//when monsteralive is false that means its dead
                {
                    message = string.Empty;
                    Debug.WriteLine("Monster is dead");
                    score.MonsterSlainNumber++;

                    // Drop Items to item Pool
                    var myItemList = MC.DropItems(target);
                    //Add Items to the Score List
                    message = "Dropped Items";
                    foreach (var item in myItemList)
                    {
                        score.TotalItemsDropped += item.FormatOutput() + "\n";
                        message += "\n" + item.Name;
                    }
                    ItemPool.AddRange(MC.DropItems(target));

                    Debug.WriteLine(message);

                    //the dead monster will be removed from the turnorder,monster's dataset and the GameBoard as well
                    TurnOrder.Remove(target);
                    CurrMonsters.Dataset.Remove(target);
                    GameBoardRemove(target);
                    Creature tempMonster = originalMonsters.Where(a => a.Id == target.Id).FirstOrDefault();
                    score.TotalMonstersKilled += tempMonster.FormatOutput(tempMonster) + "\n";//Total monsters killed is calculated

                    message = "Monster Removed :" + target.Name;
                    Debug.WriteLine(message);

                }
            }
        }

        //this method will be called by when It's monster's turn
        public void MonsterAutoTurn(Creature monster, Creature target, Score score)
        {
            string message = string.Empty;
            GetNewLoc(monster);
            Move(monster);
            if (!CanHit(monster, target))//will get the monster's location
            {
                return;
            }
            bool hit = turn.Attack(monster, target);//monster attacks the target
            if (hit)
            {
                message = "Monster " + monster.Name + " attacks " + target.Name;
                Debug.WriteLine(message);

                int damageToDo = turn.DamageToDo(monster);//Damage for the monster will be calculated
                bool characterAlive = CC.TakeDamage(target, damageToDo);
                if (!characterAlive)
                {
                    message = string.Empty;
                    Debug.WriteLine("Character is dead");


                    // Drop Items to item Pool
                    var myItemList = CC.DropItems(target);
                    //Add Items to the Score List
                    message = "Dropped Items";
                    foreach (var item in myItemList)
                    {
                        score.TotalItemsDropped += item.FormatOutput() + "\n";
                        message += "\n" + item.Name;
                    }
                    ItemPool.AddRange(myItemList);//Dropped items will added to the ItemPool
                    Debug.WriteLine(message);

                    //the dead character will be removed from the turnorder,character's team and the GameBoard as well
                    TurnOrder.Remove(target);

                    team.Dataset.Remove(target);
                    GameBoardRemove(target);
                    //Add dead characters to the score list
                    score.TotalCharactersKilled += target.FormatOutput(target) + "\n";

                    message = "Character Removed :" + target.Name;
                    Debug.WriteLine(message);

                }
            }
        }

        public Creature AutoTarget(Creature self)
        {
           
            return GetClosestEnemy(self);
            //return c;//return a creature with c.Type == targetType
        }

        public bool CanHit(Creature creature1, Creature creature2)
        {
            int dist = GetDistance(creature1, creature2);//Checks the distance for the target1 o hit the target2
            List<string> itemIds = creature1.GetHandIDs();//Adds the items to the creature1
            int range = 1;
            for (int i = 0; i < itemIds.Count; i++)
            {
                var items = ItemsViewModel.Instance.Dataset;
                var item = items.Where(a => a.Id == itemIds[i]).FirstOrDefault();
                if (item.Range > range)
                {
                    range = item.Range;
                }
            }
            if (range >= dist)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AssignItems()//Assign items to the character
        {
            foreach(Creature character in team.Dataset)
            {
                PickupItemsFromPool(character);
            }
        }

        public void PickupItemsFromPool(Creature character)
        {
            // Have the character, walk the items in the pool, and decide if any are better than current one.

            // No items in the pool...
            if (ItemPool.Count < 1)
            {
                return;
            }
            //Gets items from the pool
            GetItemFromPoolIfBetter(character, ItemLocationEnum.Head);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.Necklass);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.PrimaryHand);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.OffHand);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.RightFinger);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.LeftFinger);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.Feet);
        }

        public void GetItemFromPoolIfBetter(Creature character, ItemLocationEnum setLocation)
        {
            //account for items only being assigned finger, but creatures having left and right fingers
            ItemLocationEnum tempLoc = setLocation;
            if(setLocation == ItemLocationEnum.LeftFinger || setLocation == ItemLocationEnum.RightFinger)
            {
                tempLoc = ItemLocationEnum.Finger;
            }
            var myList = ItemPool.Where(a => a.Location == tempLoc)
                .ToList();

            // If no items in the list, return...
            if (!myList.Any())
            {
                return;
            }

            var currentItem = character.GetItemByLocation(setLocation);
            if (currentItem == null)
            {
                // If no item in the slot then put on the first in the list
                Item item = myList.FirstOrDefault();
                character.AddItem(setLocation, item.Id);
                ItemPool.Remove(item);
                return;
            }

            foreach (var item in myList)
            {
                if (item.Value > currentItem.Value)
                {
                    // Put on the new item, which drops the one back to the pool
                    var droppedItem = character.AddItem(setLocation, item.Id);

                    // Remove the item just put on from the pool
                    ItemPool.Remove(item);

                    if (droppedItem != null)
                    {
                        // Add the dropped item to the pool
                        ItemPool.Add(droppedItem);
                    }
                }
            }
        }

        public void InitializeGameBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    GameBoard[i, j] = null;
                }
            }
            for (int i = 0; i < CurrMonsters.Dataset.Count(); i++)//assign the monsters their positions in the gameboard
            {
                GameBoard[0, i] = CurrMonsters.Dataset[i];
            }
            for(int i=0; i<team.Dataset.Count(); i++)//assign the characters their positions in the gameboard
            {
                GameBoard[2, i] = team.Dataset[i];
            }
        }

        public void GetNewLoc(Creature creature)//When the player wants the creature to move and attack this helps in moving the creature
        {
            //find the creature move it one place(or more) closer to the first monster/character found
            Creature enemy = GetClosestEnemy(creature);
            CreatureLocInfo creatureInfo = GetLocInfo(creature);
            CreatureLocInfo enemyInfo = GetLocInfo(enemy);
            SelectedGridCellI = creatureInfo.row;
            SelectedGridCellJ = creatureInfo.col;
            int distToMove = creature.Speed;
            //get to the right row
            while (distToMove > 0)
            {
                if (GetDistanceFromLoc(SelectedGridCellI, SelectedGridCellJ, enemyInfo.row, enemyInfo.col) == 1)//creature is already there
                {
                    break;
                }
                if (SelectedGridCellI > enemyInfo.row)//creature needs to move down
                {
                    //check if there is a creature in the way
                    if (GameBoard[SelectedGridCellI - 1, SelectedGridCellJ] == null)
                    {
                        SelectedGridCellI--;
                        distToMove--;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (SelectedGridCellI < enemyInfo.row)//creature needs to move up
                {
                    //check if there is a creature in the way
                    if (GameBoard[SelectedGridCellI + 1, SelectedGridCellJ] == null)
                    {
                        SelectedGridCellI++;
                        distToMove--;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            //get to the right colum
            while (distToMove > 0)
            {
                if (GetDistanceFromLoc(SelectedGridCellI, SelectedGridCellJ, enemyInfo.row, enemyInfo.col) == 1)//creature is already there
                {
                    break;
                }
                if (SelectedGridCellJ > enemyInfo.col)//creature needs to move down
                {
                    //check if there is a creature in the way
                    if (GameBoard[SelectedGridCellI, SelectedGridCellJ - 1] == null)
                    {
                        SelectedGridCellJ--;
                        distToMove--;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (SelectedGridCellJ < enemyInfo.col)//creature needs to move up
                {
                    //check if there is a creature in the way
                    if (GameBoard[SelectedGridCellI, SelectedGridCellJ + 1] == null)
                    {
                        SelectedGridCellJ++;
                        distToMove--;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

        }

        public void Move(Creature creature)
        {
            CreatureLocInfo currLoc = GetLocInfo(creature);
            int distToMove = GetDistanceFromLoc(SelectedGridCellI, SelectedGridCellJ, currLoc.row, currLoc.col);
            //verfy that the creature can move to the current location
            if ((GameBoard[SelectedGridCellI, SelectedGridCellJ] == null) && distToMove <= creature.Speed)
            {
                //move the creature to a new spot on the game board
                GameBoard[SelectedGridCellI, SelectedGridCellJ] = creature;
                GameBoard[currLoc.row, currLoc.col] = null;
            }
        }

        //this will return the closest enemy creature to the creature passed in
        private Creature GetClosestEnemy(Creature creature)
        {
            CreatureLocInfo info = GetLocInfo(creature);
          
            return GetClosestEnemy(info);
        }

        private void GameBoardRemove(Creature creature) //When any creature is dead it removes that creature from the Gameboard
        {
            CreatureLocInfo info = GetLocInfo(creature);
            GameBoard[info.row, info.col] = null;
        }
        private CreatureLocInfo GetLocInfo(Creature creature)//Give the Location info
        {
            CreatureLocInfo info = new CreatureLocInfo();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if(GameBoard[i, j] != null)
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
            }
            return info;
        }

        private Creature GetClosestEnemy(CreatureLocInfo info)
        {
            Creature foundEnemy = null;
            //enemy away distance is defined as: how many column away + how many row away

            int distance = 0,minDist = 50;
            // List<CreatureLocInfo> distance = new List<CreatureLocInfo>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if(GameBoard[i, j] != null && ((GameBoard[i, j].Type) == (1- info.type)))//Checks the creature distance and provides if the other creature can attack or not
                    {
                        distance = (Math.Abs(info.row - i) + (Math.Abs(info.col - j)));
                        if (minDist >= distance)
                        {
                            minDist = distance;
                            foundEnemy = GameBoard[i, j];
                        }
                    }
                }

            }

            return foundEnemy;
        }

        public int GetDistance(Creature creature1, Creature creature2)
        {
            //will return the distance between creature1 and creature2 and ensure that if they are in neighboring squares it will return 1
            CreatureLocInfo info1 = GetLocInfo(creature1);
            CreatureLocInfo info2 = GetLocInfo(creature2);

            return (Math.Abs(info1.row - info2.row) + (Math.Abs(info1.col - info2.col)));
        }

        public int GetDistanceFromLoc(int r1, int c1, int r2, int c2) //Calculates the distance
        {
            int dist = (Math.Abs(r1 - r2) + (Math.Abs(c1 - c2)));
            return dist;
        }
    }

    public class CreatureLocInfo //need these attributes for the Locationinfo
    {
        public string ID;
        public int row;
        public int col;
        public int type;
    }

    public class CompareByAllCriteria : IComparer<Creature>//Compares the creature depending on the attributes
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

    public class CompareByLevel : IComparer<Creature> //Compare the creatures Level to attack
    {
        public int Compare(Creature x, Creature y) => x.Level.CompareTo(y.Level);
    }

    public class CompareByXP : IComparer<Creature>//Compare the creatures using the XP
    {
        public int Compare(Creature x, Creature y) => x.XP.CompareTo(y.XP);
    }

    public class CompareByName : IComparer<Creature>//Compare the creatures using name
    {
        public int Compare(Creature x, Creature y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal);
    }
}
