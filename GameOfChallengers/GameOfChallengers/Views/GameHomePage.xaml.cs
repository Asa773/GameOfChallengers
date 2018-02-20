using System;
using System.Collections.Generic;

using Xamarin.Forms;
using GameOfChallengers.Views.Items;
using GameOfChallengers.Views.Monsters;
using GameOfChallengers.Views.Character;
using GameOfChallengers.Views.Scores;
using GameOfChallengers.Views.Battle;


namespace GameOfChallengers.Views
{
    public partial class GameHomePage : ContentPage
    {
        public GameHomePage()
        {
            InitializeComponent();
        }


        private async void Help_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new HelpPage());
        }

        private async void About_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }

        private async void Play_Command(object sender, EventArgs e)
        {            

            await Navigation.PushAsync(new BattleScreen());
        }

        private async void Characters_Command(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharactersPage());
        }

        private async void Monsters_Command(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MonsterPage());
        }

        private async void Items_Command(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemsPage());
        }

        private async void Scores_Command(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScorePage());
        }

    }
}
