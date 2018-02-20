﻿using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GameOfChallengers.Models;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Views.Scores;

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
    }
}