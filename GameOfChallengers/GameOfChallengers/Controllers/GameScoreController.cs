using GameOfChallengers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class GameScoreController
    {
        Creature[] Gamboard;//this is the gameboard
        Score GameScore;//this is the Score for this game

        //there will be one controller per type and the specific creature will be passed in to the controller methods
        CharacterController Character = new CharacterController();
        MonsterController Monster = new MonsterController();
        //thinking those will have to be passed to the other two controllers, but for now not sure how to implement that

        /*
         * there should probably be some kind of Start() method to call the battle controller
        */
        
        public Score ReportScore(List<Creature> team)
        {
            //the final score will be total XP + # of turns + # of monsters killed
            //this method will report the final score as well as the "Battle History" metadata
            //the metadata is the variables at the top of the page as well as the characters' stats
            Score Score = null;
            return Score;
        }
    }
}
