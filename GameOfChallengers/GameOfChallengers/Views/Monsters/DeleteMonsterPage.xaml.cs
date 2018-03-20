 using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Models;

namespace GameOfChallengers.Views.Monsters
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DeleteMonsterPage : ContentPage
	{
        private CreatureDetailViewModel _viewModel;

        public Creature Data{ get; set; }

        public DeleteMonsterPage (CreatureDetailViewModel viewModel)
		{
            Data = viewModel.Data;
            viewModel.Title = "Delete " + viewModel.Title;

            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

       //deletes the monster and sents back to the Monsters page
        async void Delete_Clicked(object sender,EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteData", Data);

            // Remove MonsterDetailPage manualy
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            await Navigation.PopAsync();
        }

        // Cancel and go back a page in the navigation stack
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


    }
}