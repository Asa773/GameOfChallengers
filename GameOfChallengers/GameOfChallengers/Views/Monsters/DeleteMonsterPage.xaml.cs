using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Views.Monster;
using GameOfChallengers.Models;

namespace GameOfChallengers.Views.Monster
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DeleteMonsterPage : ContentPage
	{
        private MonsterDetailViewModel _viewModel;

        public Creature Data{ get; set; }

        public DeleteMonsterPage (MonsterDetailViewModel viewModel)
		{
            Data = viewModel.Data;
            viewModel.Title = "Delete " + viewModel.Title;

            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

       
        async void Delete_CLicked(object sender,EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteData", Data);

            
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            await Navigation.PopAsync();
        }


    }
}