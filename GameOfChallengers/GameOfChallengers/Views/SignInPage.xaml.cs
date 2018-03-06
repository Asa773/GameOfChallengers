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
           // BindingContext = name;
        }
        void signin_done (object sender, EventArgs e)
        {
            var text = ((Entry)sender).Text;
            //cast sender to access the properties of the Entry
        }
        private async void StartGame_Command(object sender, EventArgs e)
        {
           // App.currName = name.ToString();
            await Navigation.PushAsync(new GameHomePage());

        }


    }
}
