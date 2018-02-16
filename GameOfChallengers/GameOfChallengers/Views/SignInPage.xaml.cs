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
        }

        private async void StartGame_Command(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameHomePage());

        }


    }
}
