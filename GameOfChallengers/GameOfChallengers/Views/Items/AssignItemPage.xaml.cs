using System;
using System.Collections.Generic;
using GameOfChallengers.ViewModels;
using Xamarin.Forms;

namespace GameOfChallengers.Views.Items
{
    public partial class AssignItemPage : ContentPage
    {
        private TeamViewModel _viewModel;

        public AssignItemPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = TeamViewModel.Instance;
        }
    }
}
