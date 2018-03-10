using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace GameOfChallengers.Models
{
    public class Score
    {
        [PrimaryKey]
        public string Id { get; set; }// unique id for each score
        public string Name { get; set; }// name of player
        public int FinalScore { get; set; }//final score of game
        public DateTime Date { get; set; }
        public int Round { get; set; }//this will be used for scoring and to determine the level of the 6 monsters
        public int Turns { get; set; }//this will count the # of turns for the end game score
        public bool Auto { get; set; }//this is so that the score can show if the game was turn based or on auto mode
        public int TotalXP { get; set; }//this is to show the total xp gained by all characters on the score page
        //public List<Creature> TotalMonstersKilled { get; set; }//this is so the score can show how many monster were killed
        //public List<Creature> Team { get; set; }//this is so the score can show the characters' final stats
        //public List<Item> TotalItemsDropped { get; set; }//this is so the score can show all of the items that were dropped
        
        //The lists were causing problems with the database and may need to be refactord or moved out of the model

        public Score()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Now;
            Auto = false;
            //TotalMonstersKilled = null;
            //TotalItemsDropped = null;
        }

        public void Update(Score newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id and Date
            Name = newData.Name;
            FinalScore = newData.FinalScore;
            Round = newData.Round;
            Turns = newData.Turns;
            Auto = newData.Auto;
            TotalXP = newData.TotalXP;
            //TotalMonstersKilled = newData.TotalMonstersKilled;
            //TotalItemsDropped = newData.TotalItemsDropped;
        }
    }
}
