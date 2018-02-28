using System;
using System.Collections.Generic;

using Xamarin.Forms;
using GameOfChallengers.Views.Scores;
using GameOfChallengers.ViewModels;

namespace GameOfChallengers.Views.Battle
{
    public partial class AutoBattleScreen : ContentPage
    {
        public AutoBattleScreen()
        {
            InitializeComponent();
        }
        private async void Score_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScorePage());
        }
    }
}
