using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using GameOfChallengers.Models;
using GameOfChallengers.Views.Battle;
using GameOfChallengers.Views.Monsters;
using System.Linq;


namespace GameOfChallengers.ViewModels
{
    class MonstersListViewModel : BaseViewModel
    {


        private static MonstersListViewModel _instance;

        public static MonstersListViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MonstersListViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Creature> Dataset { get; set; }
        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public MonstersListViewModel()
        {

            Dataset = new ObservableCollection<Creature>();
            // LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());



            MessagingCenter.Subscribe<BattleScreen, Creature>(this, "DeleteData", (obj, data) =>
            {
                Dataset.Remove(data);
                // await DataStore.DeleteAsync_Item(data);
            });

            //MessagingCenter.Subscribe<BuildTeamPage, Creature>(this, "AddData", (obj, data) =>
            //{
            //    Dataset.Add(data);
            //    //DataStore.AddAsync_Team(data);
            //});


        }

    }
}
