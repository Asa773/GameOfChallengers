using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using GameOfChallengers.Models;
using GameOfChallengers.Views.Scores;
using System.Linq;
using GameOfChallengers.Controllers;

namespace GameOfChallengers.ViewModels
{
    public class ScoresViewModel : BaseViewModel
    {
        private static ScoresViewModel _instance;
        //created an instance of ScoresViewModel
        public static ScoresViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScoresViewModel();
                }
                return _instance;
            }
        }

        //creating dataset of scores
        public ObservableCollection<Score> Dataset { get; set; }
        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public ScoresViewModel()
        {
            Title = "Scores List";
            Dataset = new ObservableCollection<Score>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            //subsribing to methods which are called from views
            MessagingCenter.Subscribe<DeleteScorePage, Score>(this, "DeleteData", async (obj, data) =>
            {
                Dataset.Remove(data);
                await DataStore.DeleteAsync_Score(data);
            });

            //subsribing to methods which are called from views
            MessagingCenter.Subscribe<NewScorePage, Score>(this, "AddData", async (obj, data) =>
            {
                //adding data to dataset
                Dataset.Add(data);
                await DataStore.AddAsync_Score(data);
            });

            //subsribing to methods which are called from views
            MessagingCenter.Subscribe<GameScoreController, Score>(this, "AddData", async (obj, data) =>
            {
               
                Dataset.Add(data);
                await DataStore.AddAsync_Score(data);
            });

            //subsribing to methods which are called from views
            MessagingCenter.Subscribe<EditScorePage, Score>(this, "EditData", async (obj, data) =>
            {
                // Find the Item, then update it
                var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
                if (myData == null)
                {
                    return;
                }

                myData.Update(data);
                await DataStore.UpdateAsync_Score(myData);

                _needsRefresh = true;

            });
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
                var dataset = await DataStore.GetAllAsync_Score(true);
                foreach (var data in dataset)
                {
                    Dataset.Add(data);
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