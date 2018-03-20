﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using GameOfChallengers.Models;
using GameOfChallengers.Services;

namespace GameOfChallengers.ViewModels
{
    public class TeamViewModel : BaseViewModel
    {
        private static TeamViewModel _instance;

        // Make this a singleton so it only exist one time because holds all the data records in memory
        public static TeamViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TeamViewModel();
                }
                return _instance;
            }
        }

        //setting the dataset for Team
        public ObservableCollection<Creature> Dataset { get; set; }

        private bool _needsRefresh;

        public TeamViewModel()
        {
            //a list of the characters on the team
            //a list of actuall characters to use in the current game
            //the second list will reload from the first at the start of each game
            //the first list will be reset by the build team actions

            Title = "Current Team";
            Dataset = new ObservableCollection<Creature>();
        }



        // Return True if a refresh is needed
        // It sets the refresh flag to false
        public bool NeedsRefresh()
        {
            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }

            return false;
        }

        // Sets the need to refresh
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

        //loading the characters in the team
        public void LoadTeam()
        {
            Dataset.Clear();
            if (CharactersViewModel.Instance.Dataset.Count == 0)
            {
                CharactersViewModel.Instance.LoadDataCommand.Execute(null);
            }
            else if (CharactersViewModel.Instance.NeedsRefresh())
            {
                CharactersViewModel.Instance.LoadDataCommand.Execute(null);
            }
            var dataset = CharactersViewModel.Instance.GetAllCreatures();
            int teamCount = 0;
            foreach (var data in dataset)
            {
                if ((data.Type == 0) && (teamCount < 6) && (data.OnTeam))//the creature is a character, the team is not full, and it is on the current team
                {
                    teamCount++;
                    Creature newOne = new Creature();
                    newOne.Update(data);
                    newOne.Id = Guid.NewGuid().ToString();
                    Dataset.Add(newOne);
                }
            }
            if (teamCount < 6)
            {
                foreach (var data in dataset)//if the team is not full more characters must be added
                {
                    if ((data.Type == 0) && (teamCount < 6) && (!data.OnTeam))//the creature is a character, the team is not full, and the character is not in the team
                    {
                        teamCount++;
                        data.OnTeam = true;
                        Creature newOne = new Creature();
                        newOne.Update(data);
                        newOne.Id = Guid.NewGuid().ToString();
                        Dataset.Add(newOne);
                    }
                }
            }
            if (teamCount < 6)//if you didn't make enough characters you get some sucky ones
            {
                int numOfSuckyCharacters = 0;
                for (int i = teamCount; i < 6; i++)
                {
                    numOfSuckyCharacters++;
                    Creature character = new Creature();
                    character.Id = Guid.NewGuid().ToString();
                    character.Type = 0;
                    character.OnTeam = true;
                    character.Name = "Sucky Character " + numOfSuckyCharacters.ToString();
                    character.Attack = 1;
                    character.Defense = 1;
                    character.Speed = 1;
                    character.MaxHealth = 1;
                    character.CurrHealth = character.MaxHealth;
                    character.ImageURI = "superman.jpeg";
                    Dataset.Add(character);
                    MessagingCenter.Send(this, "AddData", character);
                }
            }
        }
    }
}
