using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Models;
using GameOfChallengers.Views.Battle;
using System.Collections.ObjectModel;

namespace GameOfChallengers.Views.Character
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuildTeamPage : ContentPage
    {
        public CharactersViewModel viewModel;
        public Creature Data { get; set; }
        public List<Creature> newTeam = new List<Creature>();

        public BuildTeamPage()
        {
            InitializeComponent();

            //make sure that the CharactersViewModel has its data loaded
            if (CharactersViewModel.Instance.Dataset.Count == 0)
            {
                CharactersViewModel.Instance.LoadDataCommand.Execute(null);
            }
            else if (CharactersViewModel.Instance.NeedsRefresh())
            {
                CharactersViewModel.Instance.LoadDataCommand.Execute(null);
            }

            BindingContext = viewModel = CharactersViewModel.Instance;
        }

        private async void SaveTeam_Clicked(object sender, EventArgs e)
        {
            Creature c1 = (Creature)CharacterPicker1.SelectedItem;
            if (c1 != null)
            {
                newTeam.Add(c1);
            }
            Creature c2 = (Creature)CharacterPicker2.SelectedItem;
            if (c2 != null)
            {
                newTeam.Add(c2);
            }
            Creature c3 = (Creature)CharacterPicker3.SelectedItem;
            if (c3 != null)
            {
                newTeam.Add(c3);
            }
            Creature c4 = (Creature)CharacterPicker4.SelectedItem;
            if (c4 != null)
            {
                newTeam.Add(c4);
            }
            Creature c5 = (Creature)CharacterPicker5.SelectedItem;
            if (c5 != null)
            {
                newTeam.Add(c5);
            }
            Creature c6 = (Creature)CharacterPicker6.SelectedItem;
            if (c6 != null)
            {
                newTeam.Add(c6);
            }
            
            for (int i=0; i<viewModel.Dataset.Count; i++)
            {
                viewModel.Dataset[i].OnTeam = false;
            }
            foreach(Creature c in newTeam)
            {
                c.OnTeam = true;
            }
            await Navigation.PushAsync(new BattleScreen());
        }

        private async void AutoSelect_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BattleScreen());
        }
    }
}
 

