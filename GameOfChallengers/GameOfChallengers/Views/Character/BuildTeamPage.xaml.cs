using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameOfChallengers.Views.Character
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuildTeamPage : ContentPage
    {
        public BuildTeamPage()
        {
            InitializeComponent();
        }

        public class LabelGridCode : ContentPage
            {
            public LabelGridCode()

        {
            var grid = new Xamarin.Forms.Grid();
                var topLeft = new Xamarin.Forms.Image { Source = "icon.png" };
                var topRight = new Xamarin.Forms.Image { Source = "icon.png" };
                var bottomLeft = new Xamarin.Forms.Image { Source = "icon.png" };
                var bottomRight = new Xamarin.Forms.Image { Source = "icon.png" };
                //var topLeft = new Label { Text = "Top Left" };
                //var topRight = new Label { Text = "Top Right" };
                //var bottomLeft = new Label { Text = "Bottom Left" };
                //var bottomRight = new Label { Text = "Bottom Right" };

                grid.RowDefinitions.Add(new Xamarin.Forms.RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.RowDefinitions.Add(new Xamarin.Forms.RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new Xamarin.Forms.ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new Xamarin.Forms.ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                Content = grid;


        }
       }


        private async void SaveTeam_Clicked(object sender, EventArgs e)
        {

        }

        private async void AutoSelect_Clicked(object sender, EventArgs e)
        {

        }
    }
}