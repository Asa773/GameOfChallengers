using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using GameOfChallengers.Models;
using GameOfChallengers.Views.Battle;
using GameOfChallengers.Views.Character;
using System.Linq;


namespace GameOfChallengers.ViewModels
{
    class TeamViewModel : BaseViewModel
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
            
            Dataset = new ObservableCollection<Creature>();
           // LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());



            MessagingCenter.Subscribe<BattleScreen, Creature>(this, "DeleteData",  (obj, data) =>
            {
                Dataset.Remove(data);
               // await DataStore.DeleteAsync_Item(data);
            });

            MessagingCenter.Subscribe<BuildTeamPage, Creature>(this, "AddData",  (obj, data) =>
            {
                Dataset.Add(data);
                 //DataStore.AddAsync_Team(data);
            });


        }



        // Sets the need to refresh
       

    

    }
}
