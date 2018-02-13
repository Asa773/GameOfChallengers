using GameOfChallengers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class GameScoreController
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
        //thinking those will have to be passed to the other two controllers, but for now not sure how to implement that

        /*
         * there should probably be some kind of Start() method to call the battle controller
        */
        
        public string[] ReportScore(List<Creature> team)
        {
            //the final score will be total XP + # of turns + # of monsters killed
            //this method will report the final score as well as the "Battle History" metadata
            //the metadata is the variables at the top of the page as well as the characters' stats
            string[] Score = null;
            return Score;
        }
    }
}
