using System;
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

        public ObservableCollection<Creature> Dataset { get; set; }
        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public TeamViewModel()
        {
            Title = "Current Team";
            Dataset = new ObservableCollection<Creature>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());
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
        
        public void LoadData()
        {
            Dataset.Clear();
            var dataset = CharactersViewModel.Instance.GetAllCreatures();
            int teamCount = 0;
            foreach (var data in dataset)
            {
                if ((data.Type == 0) && (teamCount < 6) && data.OnTeam)//the creature is a character, the team is not full, and it is on the current team
                {
                    teamCount++;
                    Dataset.Add(data);
                }
            }
        }
        async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Dataset.Clear();
                //var dataset = await SQLDataStore.GetAllAsync_Creature(true);

                var dataset = CharactersViewModel.Instance.GetAllCreatures();
                int teamCount = 0;
                foreach (var data in dataset)
                {
                    if ((data.Type == 0) && (teamCount < 6) && data.OnTeam)//the creature is a character, the team is not full, and it is on the current team
                    {
                        teamCount++;
                        Dataset.Add(data);
                    }
                }
                if (teamCount < 6)
                {
                    foreach (var data in dataset)//if the team is not full more characters must be added
                    {
                        if ((data.Type == 0) && (teamCount < 6) && !Dataset.Contains(data))//the creature is a character, the team is not full, and the character is not in the team
                        {
                            teamCount++;
                            data.OnTeam = true;
                            Dataset.Add(data);
                        }
                    }
                }
                if (teamCount < 6)//if you didn't make enough characters you get some sucky ones
                {
                    int numOfSuckyCharacters = 0;
                    for (int i=teamCount; i<6; i++)
                    {
                        numOfSuckyCharacters++;
                        Creature character = new Creature();
                        character.Type = 0;
                        character.OnTeam = true;
                        character.Name = "Sucky Character " + numOfSuckyCharacters.ToString();
                        character.Attack = 1;
                        character.Defense = 1;
                        character.Speed = 1;
                        character.MaxHealth = 1;
                        character.CurrHealth = character.MaxHealth;
                        Dataset.Add(character);
                        await DataStore.AddAsync_Creature(character);
                    }
                }
                //                      ***temp for demo***
                foreach (var data in Dataset)
                {
                    data.RHandItemID = "bow";
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            finally
            {
                IsBusy = false;
            }
        }
    }
}
