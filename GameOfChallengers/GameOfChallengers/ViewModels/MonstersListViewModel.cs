using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using GameOfChallengers.Models;
using GameOfChallengers.Views.Battle;
using GameOfChallengers.Views.Monsters;
using System.Linq;
using System.Collections.Generic;

namespace GameOfChallengers.ViewModels
{
    class MonstersListViewModel : BaseViewModel
    {
        private static int round = 0;

        //should probably refactor into a model or something because it is used in more than one place
        List<LevelingUp> lp = new List<LevelingUp>
        {
            new LevelingUp{XP = 0,     Level = 0, Attack = 0,Defense = 0, Speed = 0},
            new LevelingUp{XP = 0,     Level = 1, Attack = 1,Defense = 1, Speed = 1},
            new LevelingUp{XP = 300,   Level = 2, Attack = 1,Defense = 2, Speed = 1},
            new LevelingUp{XP = 900,   Level = 3, Attack = 2,Defense = 3, Speed = 1},
            new LevelingUp{XP = 2700,  Level = 4, Attack = 2,Defense = 3, Speed = 1},
            new LevelingUp{XP = 6500,  Level = 5, Attack = 2,Defense = 4, Speed = 2},
            new LevelingUp{XP = 14000, Level = 6, Attack = 3,Defense = 4, Speed = 2},
            new LevelingUp{XP = 23000, Level = 7, Attack = 3,Defense = 5, Speed = 2},
            new LevelingUp{XP = 34000, Level = 8, Attack = 3,Defense = 5, Speed = 2},
            new LevelingUp{XP = 48000, Level = 9, Attack = 3,Defense = 5, Speed = 2},
            new LevelingUp{XP = 64000, Level = 10,Attack = 4,Defense = 6, Speed = 3},
            new LevelingUp{XP = 85000, Level = 11,Attack = 4,Defense = 6, Speed = 3},
            new LevelingUp{XP = 100000,Level = 12,Attack = 4,Defense = 6, Speed = 3},
            new LevelingUp{XP = 120000,Level = 13,Attack = 4,Defense = 7, Speed = 3},
            new LevelingUp{XP = 140000,Level = 14,Attack = 5,Defense = 7, Speed = 3},
            new LevelingUp{XP = 165000,Level = 15,Attack = 5,Defense = 7, Speed = 4},
            new LevelingUp{XP = 195000,Level = 16,Attack = 5,Defense = 8, Speed = 4},
            new LevelingUp{XP = 225000,Level = 17,Attack = 5,Defense = 8, Speed = 4},
            new LevelingUp{XP = 265000,Level = 18,Attack = 6,Defense = 8, Speed = 4},
            new LevelingUp{XP = 305000,Level = 19,Attack = 7,Defense = 9, Speed = 4},
            new LevelingUp{XP = 355000,Level = 20,Attack = 8,Defense = 10,Speed = 5},
        };

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

        private MonstersViewModel viewModel;

        private bool _needsRefresh;

        public MonstersListViewModel()
        {
            
            Title = "Current Monsters";
            Dataset = new ObservableCollection<Creature>();
            viewModel = MonstersViewModel.Instance;
        }

        public void setRound(int roundNum)
        {
            round = roundNum;
        }

        public void setMonsters()
        {
            Dataset.Clear();
            //if (MonstersViewModel.Instance.Dataset.Count == 0)
            //{
            //    MonstersViewModel.Instance.LoadDataCommand.Execute(null);
            //}
            //else if (MonstersViewModel.Instance.NeedsRefresh())
            //{
            //    MonstersViewModel.Instance.LoadDataCommand.Execute(null);
            //}
            //if (viewModel.Dataset.Count == 0)
            //{
            //    viewModel.LoadDataCommand.Execute(null);
            //}
            //else if (viewModel.NeedsRefresh())
            //{
            //    viewModel.LoadDataCommand.Execute(null);
            //}
            MonstersViewModel.Instance.LoadDataCommand.Execute(null);
            var dataset = MonstersViewModel.Instance.GetAllCreatures();
            var tempDataset = new List<Creature>();
            int dateSeed = DateTime.Now.Millisecond;
            Random rand = new Random(dateSeed);
            foreach (var data in dataset)
            {
                if (data.Type == 1)// just Monsters
                {
                    Creature newOne = new Creature();
                    newOne.Update(data);
                    
                    tempDataset.Add(newOne);

                }
            }
            for (int i = 0; i < 6; i++)
            {
                int Round = round;
                if (round > 20)
                {
                    Round = 20;
                }
                int index = rand.Next(tempDataset.Count);
                Creature monster = new Creature();
                monster.Update(tempDataset[index]);//get a random monster type
                monster.Id = "monster" + i.ToString();//Guid.NewGuid().ToString();
                monster.Alive = true;
                monster.Level = Round;
                monster.XP = lp[Round].XP;
                monster.Attack = lp[Round].Attack;
                monster.Defense = lp[Round].Defense;
                monster.Speed = lp[Round].Speed;
                monster.MaxHealth = rand.Next(1, 11) * Round;
                if(round == 1)
                {
                    monster.MaxHealth = 1;
                    monster.XP = 100;
                }
                monster.CurrHealth = monster.MaxHealth;

                var items = ItemsViewModel.Instance.Dataset;
                int itemCount = 0;
                while(itemCount < 3)
                {
                    var item = items[rand.Next(items.Count)];
                    var itemLocation = item.Location;
                    if (item.Location == ItemLocationEnum.Finger)
                    {
                        itemLocation = ItemLocationEnum.RightFinger;
                    }
                    if(monster.GetItemByLocation(itemLocation) == null)
                    {
                        monster.AddItem(itemLocation, item.Id);
                        itemCount++;
                    }
                }
                //load unique drop
                var uItem = items[rand.Next(items.Count)];
                monster.UniqueItem = uItem.Id;
                Dataset.Add(monster);
            }
        }

        public bool NeedsRefresh()
        {
            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }

            return false;
        }

        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }
    }
}
