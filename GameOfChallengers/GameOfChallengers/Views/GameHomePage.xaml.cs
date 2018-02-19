using System;
using System.Collections.Generic;

using Xamarin.Forms;

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
            //await Navigation.PushAsync(new NewItemPage());
        }

        private async void About_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }

        private async void Play_Command(object sender, EventArgs e)
        {            

        }

        private async void Characters_Command(object sender, EventArgs e)
        {

        }

        private async void Monsters_Command(object sender, EventArgs e)
        {

        }

        private async void Items_Command(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new ItemsPage());
        }

        private async void Scores_Command(object sender, EventArgs e)
        {

        }

    }
}
