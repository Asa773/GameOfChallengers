using GameOfChallengers.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Services;
using GameOfChallengers.Models;


namespace GameOfChallengers.Views
{
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();

            name.Text = GameGlobals.PlayerName;
            BindingContext = name;


        }

        private async void StartGame_Command(object sender, EventArgs e)
        {
            GameGlobals.PlayerName = name.Text;//Saving the player name 
            await Navigation.PushAsync(new GameHomePage());//Goes to the game home page
            CharactersViewModel.Instance.LoadDataCommand.Execute(null);//Loads the characters
            MonstersViewModel.Instance.LoadDataCommand.Execute(null);//Loads the monsters
            ScoresViewModel.Instance.LoadDataCommand.Execute(null);//Loads the scores
            ItemsViewModel.Instance.LoadDataCommand.Execute(null);//Loads the items

        }


    }
}
