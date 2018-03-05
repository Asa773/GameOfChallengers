﻿using GameOfChallengers.Models;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameOfChallengers.Controllers
{
    public class GameScoreController
    {
        int round = 0;
        Score GameScore;//this is the Score for this game
        TeamViewModel Team;//this is the team of six characters for this game

        public GameScoreController()
        {
            Score GameScore = new Score();
           
            TeamViewModel.Instance.LoadData();
            TeamViewModel Team = TeamViewModel.Instance;
        }

        public async Task<bool> Start(bool auto)
        {
            TeamViewModel Team = TeamViewModel.Instance;
            round++;
            BattleController battle = new BattleController(round);
            while (Team.Dataset.Count > 0)
            {
                if (auto)
                {
                    
                    GameScore = battle.AutoBattle(GameScore);
                    GameScore.Auto = true;
                }
                else
                {
                    GameScore.Auto = false;
                    battle.Battle();
                }
            }
            return await ReportScore();
        }
        
        public async Task<bool> ReportScore()
        {
            //the final score will be total XP + # of turns + # of monsters killed
            //this method will report the final score as well as the "Battle History" metadata
            //the metadata is the variables at the top of the page as well as the characters' stats

            //load GameScore
            GameScore.Name = App.currName;//***NOT working right now***
            GameScore.Date = DateTime.Now;//to set the time to when the game was finished
            GameScore.Round = round;
            //GameScore.Team.AddRange(Team);
            GameScore.FinalScore = GameScore.TotalXP;

            return await SQLDataStore.Instance.AddAsync_Score(GameScore);
        }
    }
}
