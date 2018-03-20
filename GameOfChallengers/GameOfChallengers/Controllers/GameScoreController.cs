using GameOfChallengers.Models;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using GameOfChallengers.Views.Battle;

namespace GameOfChallengers.Controllers
{
    public class GameScoreController
    {
        int round = 0;
        public Score GameScore;//this is the Score for this game
        TeamViewModel Team;//this is the team of six characters for this game
        public BattleController battle = new BattleController();//this is the current running battle(round)

        public GameScoreController()
        {
            GameScore = new Score(); //calls the score

            TeamViewModel.Instance.LoadTeam();
            Team = TeamViewModel.Instance;//team will be assigned
        }

        // Mike, I made this at the start of the game, rather than lower.  
        // That way it is only called once.


        public void LoadDataSets()
        {

            var myData = MonstersViewModel.Instance;
            var canExecute = myData.LoadDataCommand.CanExecute(null);
            myData.LoadDataCommand.Execute(null);

            var myItemViewModel = ItemsViewModel.Instance;
            canExecute = myItemViewModel.LoadDataCommand.CanExecute(null);
            myItemViewModel.LoadDataCommand.Execute(null);

        }


        public Score Start(bool auto)
        {
            LoadDataSets();

            Team = TeamViewModel.Instance;

            if (auto)
            {
                while (Team.Dataset.Count > 0)//checks if the character's team is not zero
                {
                    round++;//round value will increase
                    battle.SetBattleController(round);//Batllecontroller will start to work
                    GameScore = battle.AutoBattle(GameScore);//GameScore will be assigned
                    GameScore.Auto = true;
                    
                }
            }
            else
            {
                round++;//round value will increase
                battle.SetBattleController(round);//Maunal battle will start when its not auot
                //GameScore = battle.Battle(GameScore);
                GameScore.Auto = false;
            }
            return ReportScore();
        }
        
        public Score ReportScore()
        {
            //the final score will be total XP + # of turns + # of monsters killed
            //this method will report the final score as well as the "Battle History" metadata
            //the metadata is the variables at the top of the page as well as the characters' stats

            //load GameScore
            GameScore.Name = GameGlobals.PlayerName;
            GameScore.Date = DateTime.Now;//to set the time to when the game was finished
            GameScore.Round = round;
            //GameScore.Team.AddRange(Team);
            GameScore.FinalScore = GameScore.TotalXP;
            MessagingCenter.Send(this, "AddData", GameScore);
            return GameScore;
        }
    }
}
