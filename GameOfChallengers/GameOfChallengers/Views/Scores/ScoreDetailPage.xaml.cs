using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GameOfChallengers.Models;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Controllers;

namespace GameOfChallengers.Views.Scores
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScoreDetailPage : ContentPage
	{
        private ScoreDetailViewModel _viewModel;
        public Score Data { get; set; }

        public ScoreDetailPage(ScoreDetailViewModel viewModel)
        {
            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

        public ScoreDetailPage()
        {
            InitializeComponent();
            
            Data = new Score
            {
                //set default attributes
                Name = "Player name",
                Id = Guid.NewGuid().ToString(),
                FinalScore = 0,
                Round = 1,
                Date = DateTime.Now,
                TotalXP = 10,
                Turns = 10,
                Auto = true,
            };

            _viewModel = new ScoreDetailViewModel(Data);
            BindingContext = _viewModel;
        }


        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditScorePage(_viewModel));//Editing the monster page
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new DeleteScorePage(_viewModel));//deleting the monster page

        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();//goes back to the previous page
        }
    }
}