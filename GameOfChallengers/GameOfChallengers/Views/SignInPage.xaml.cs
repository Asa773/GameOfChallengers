using GameOfChallengers.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

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
            GameGlobals.PlayerName = name.Text;
            await Navigation.PushAsync(new GameHomePage());

        }


    }
}
