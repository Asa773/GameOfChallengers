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
            BindingContext = name;
        }

        void Signin_done(object sender, EventArgs e)
        {
            //cast sender to access the properties of the Entry
            var text = ((Entry)sender).Text;
        }

        private async void StartGame_Command(object sender, EventArgs e)
        {
            App.currName = name.ToString();
            await Navigation.PushAsync(new GameHomePage());

        }


    }
}
