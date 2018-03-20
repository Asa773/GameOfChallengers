using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using GameOfChallengers.Models;
using GameOfChallengers.Views.Character;
using System.Linq;
using System.Collections.Generic;

namespace GameOfChallengers.ViewModels
{
    public class CharactersViewModel : BaseViewModel
    {
        private static CharactersViewModel _instance;

        public static CharactersViewModel Instance
        {
            //created an instance of charactersViewModel
            get
            {
                if (_instance == null)
                {
                    _instance = new CharactersViewModel();
                }
                return _instance;
            }
        }
        //character observable collection is declared
        public ObservableCollection<Creature> Dataset { get; set; }
        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public CharactersViewModel()
        {
            Title = "Characters List";
            Dataset = new ObservableCollection<Creature>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand()); //loading the characters

            //subsribing to methods which are called from views
            MessagingCenter.Subscribe<DeleteCharacter, Creature>(this, "DeleteData", async (obj, data) =>
            {
                //deleting data from the dataset
                Dataset.Remove(data);
                await DataStore.DeleteAsync_Creature(data);
            });
            //subsribing to methods which are called from views
            MessagingCenter.Subscribe<NewCharacter, Creature>(this, "AddData", async (obj, data) =>
            {
                //adding the data to dataset
                Dataset.Add(data);
                await DataStore.AddAsync_Creature(data);
            });
            //subsribing to methods which are called from views

            MessagingCenter.Subscribe<TeamViewModel, Creature>(this, "AddData", async (obj, data) =>
            {
                //adding the data in team dataset
                Dataset.Add(data);
                await DataStore.AddAsync_Creature(data);
            });
            //subsribing to methods which are called from views
            MessagingCenter.Subscribe<EditCharacter, Creature>(this, "EditData", async (obj, data) =>
            {
                // Find the Item, then update it
                var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
                if (myData == null)
                {
                    return;
                }

                myData.Update(data);
                await DataStore.UpdateAsync_Creature(myData);

                _needsRefresh = true;

            });
        }



        //getting the list of all characters from the dataset
        public List<Creature> GetAllCreatures()
        {
            var myReturn = new List<Creature>();
            foreach (var item in Dataset)
            {
                myReturn.Add(item);
            }

            return myReturn;
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

        async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Dataset.Clear();
                var dataset = await DataStore.GetAllAsync_Creature(true);
                foreach (var data in dataset)
                {
                    if(data.Type == 0)// just Characters
                    {
                        Dataset.Add(data);
                    }
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

    //used for loading images in character picker view
    public static class CharacterImagesList
    {

        public static List<String> GetListCharacterImages
        {
            get
            {
               
                List<String> characterImages = new List<String>
                            {
                             
                              "batman.jpeg",
                              "Captain-America.jpeg",
                              "goldenfighter.jpg",
                              "Hulk.jpeg",
                               "ironman.jpeg",
                                "kingsmen.jpeg",
                                "knight.jpeg",
                                "spiderman.jpeg",
                                 "superman.jpeg",
                                 "warrior.jpeg",
                                 "reddragon.jpeg",
                             };
                return characterImages;
            }
        }
    }

}