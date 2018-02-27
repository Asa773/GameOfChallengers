using GameOfChallengers.Models;
using GameOfChallengers.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChallengers.Controllers
{
    class GameScoreController
    {
        int round = 0;
        Score GameScore;//this is the Score for this game

        //there will be one controller per type and the specific creature will be passed in to the controller methods
        CharacterController Character = new CharacterController();
        MonsterController Monster = new MonsterController();

        TeamViewModel Team;

        /*
         * there should probably be some kind of Start() method to call the battle controller
        */

        public void Start(bool auto)
        {
            round++;
            //fill team
            BattleController battle = new BattleController(Team, round);
            while (Team.Dataset.Count > 0)
            {
                if (auto)
                {
                    battle.AutoBattle(Team);
                }
                else
                {
                    battle.Battle(Team);
                }
            }
            ReportScore(Team);
        }
        
        public void ReportScore(TeamViewModel team)
        {
            //the final score will be total XP + # of turns + # of monsters killed
            //this method will report the final score as well as the "Battle History" metadata
            //the metadata is the variables at the top of the page as well as the characters' stats

            //load GameScore
            GameScore.Name = App.currName;

        }
    }
}
