﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using GameOfChallengers.Controllers;

namespace GameOfChallengers.Models
{
    public class Score
    {
        [PrimaryKey]
        public string Id { get; set; }// unique id for each score
        public string Name { get; set; }// name of player
        public int FinalScore { get; set; }//final score of game
        public DateTime Date { get; set; }//date that the score was made/earned
        public int Round { get; set; }//this will be used for scoring and to determine the level of the 6 monsters
        public int Turns { get; set; }//this will count the # of turns for the end game score
        public bool Auto { get; set; }//this is so that the score can show if the game was turn based or on auto mode
        public int TotalXP { get; set; }//this is to show the total xp gained by all characters on the score page
        public string TotalMonstersKilled { get; set; }//this is so the score can show how many monster were killed
        public string TotalCharactersKilled { get; set; }//this is so the score can show the characters' final stats
        public string TotalItemsDropped { get; set; }//this is so the score can show all of the items that were dropped
        public int MonsterSlainNumber { get; set; }// The count of monsters slain during battle

        public Score()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Now;
            Auto = false;


            TotalCharactersKilled = null;

            TotalMonstersKilled = null;
            TotalItemsDropped = null;
            MonsterSlainNumber = 0;
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
            TotalCharactersKilled = newData.TotalCharactersKilled;
            TotalMonstersKilled = newData.TotalMonstersKilled;
            TotalItemsDropped = newData.TotalItemsDropped;
            MonsterSlainNumber = newData.MonsterSlainNumber;
        }

       //List for the Characters killed
        public bool AddCharacterToList(Creature character)
        {
            if (character == null)
            {
                return false;
            }

            TotalCharactersKilled += character.FormatOutput(character) + "\n";
            return true;
        }

        // All a monster to the list of monsters and their stats
        public bool AddMonsterToList(Creature monster)
        {
            if (monster == null)
            {
                return false;
            }

            TotalMonstersKilled += monster.FormatOutput(monster) + "\n";
            return true;
        }

        // All an item to the list of items for score and their stats
        public bool AddItemToList(Item data)
        {
            if (data == null)
            {
                return false;
            }

            TotalItemsDropped += data.FormatOutput() + "\n";
            return true;
        }
    }  
}
