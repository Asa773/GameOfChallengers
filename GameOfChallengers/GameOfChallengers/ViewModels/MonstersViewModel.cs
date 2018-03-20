using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using GameOfChallengers.Models;
using GameOfChallengers.Views.Monsters;
using System.Linq;
using System.Collections.Generic;

namespace GameOfChallengers.ViewModels
{
    public class MonstersViewModel : BaseViewModel
    {
        private static MonstersViewModel _instance;


        // Make this a singleton so it only exist one time because holds all the data records in memory
        public static MonstersViewModel Instance
        {
            //created an instance of monsterViewModel
            get
            {
                if (_instance == null)
                {
                    _instance = new MonstersViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Creature> Dataset { get; set; }
        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;
        public bool NeedsRefresh()
        {
            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }

            return false;
        }
        public MonstersViewModel()
        {
            Title = "Monsters List";

            //monster observable collection is declared
            Dataset = new ObservableCollection<Creature>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());//loading the monsters

            //subsribing to methods which are called from views
            MessagingCenter.Subscribe<DeleteMonsterPage, Creature>(this, "DeleteData", async (obj, data) =>
            {
                //deleting data from the dataset
                Dataset.Remove(data);
                await DataStore.DeleteAsync_Creature(data);
            });

            //subsribing to methods which are called from views
            MessagingCenter.Subscribe<CreateMonster, Creature>(this, "AddData", async (obj, data) =>
            {
                //adding data in the dataset
                Dataset.Add(data);
                await DataStore.AddAsync_Creature(data);
            });
            //subsribing to methods which are called from views
            MessagingCenter.Subscribe<EditMonsterPage, Creature>(this, "EditData", async (obj, data) =>
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

        //getting the list of all monsters from dataset
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
                    if (data.Type == 1)// just Monsters
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

    //used for loading images in monster picker view
    public static class MonsterImagesList
    {

        public static List<String> GetListMonsterImages
        {
            get
            {

                List<String> monsterImages = new List<String>
                            {

                               "uglyface.jpeg",
                                 "maskman.jpeg",
                                "skeleton.jpeg",
                                  "hitler.jpeg",
                                  "zombie.jpeg",
                                 "bluemonster.jpeg",
                             };
                return monsterImages;
            }
        }
    }


}