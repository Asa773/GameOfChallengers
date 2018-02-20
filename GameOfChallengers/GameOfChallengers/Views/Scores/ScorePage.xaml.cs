using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GameOfChallengers.Models;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Views.Scores;
<<<<<<< HEAD
using GameOfChallengers.Views.Battle;
=======
//using GameOfChallengers.Views.Battle;
>>>>>>> 1a1ab93749068028de6b97fd9958c98f59c8f4bf

namespace GameOfChallengers.Views.Scores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScorePage : ContentPage
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        ScoreDetailViewModel _viewModel;
        

        public ScorePage()
        {
            InitializeComponent();

            var data = new Score
            {
                Name = "Score name",
                FinalScore = 0
            };

            _viewModel = new ScoreDetailViewModel(data);
            BindingContext = _viewModel;
        }

        async void TryAgain_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new BattleScreen());
        }


        async void Exit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}