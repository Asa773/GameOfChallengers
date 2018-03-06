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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace GameOfChallengers.Views.Character
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuildTeamPage : ContentPage
    {
        public CharactersViewModel viewModel;
        public CreatureDetailViewModel _viewModel;
        public ObservableCollection<Creature> Dataset { get; set; }
        public Creature Data { get; set; }

        public BuildTeamPage()
        {
            InitializeComponent();
            // characterselect.IsToggled = true;

            Data = new Creature
            {
                Name = "Character name",
                Id = Guid.NewGuid().ToString(),
                Type = 0,
                Level = 1,
                XP = 0,
                MaxHealth = 10,
                CurrHealth = 10,
                Alive = true,

            };

            string Mike = string.Empty;

            _viewModel = new CreatureDetailViewModel(Data);
           // BindingContext = _viewModel;
            BindingContext = viewModel = CharactersViewModel.Instance;
            CharacterPicker1.SelectedIndex = 0;
                CharacterPicker2.SelectedIndex = 0;
            CharacterPicker3.SelectedIndex = 0;
            CharacterPicker4.SelectedIndex = 0;
            CharacterPicker5.SelectedIndex = 0;
            CharacterPicker6.SelectedIndex = 0;
        }

       


    private async void SaveTeam_Clicked(object sender, EventArgs e)
    {
       // await Navigation.PopAsync();
            await Navigation.PushAsync(new BattleScreen());
    }



    private async void AutoSelect_Clicked(object sender, EventArgs e)
        {

            var myTest = CharacterPicker1.SelectedItem;
            var myTest1 = CharacterPicker2.SelectedItem;
            var myTest2 = CharacterPicker3.SelectedItem;
            var myTest3 = CharacterPicker4.SelectedItem;
            var myTest4 = CharacterPicker5.SelectedItem;
            var myTest5 = CharacterPicker6.SelectedItem;


            //await Navigation.PopAsync();
            await Navigation.PushAsync(new BattleScreen());
        }

        //private void select_toggled(object sender, ToggledEventArgs e)
        //{
        //    MessagingCenter.Send(this, "AddData", Data);
        //     Navigation.PopAsync();
        //}

}


}
 

