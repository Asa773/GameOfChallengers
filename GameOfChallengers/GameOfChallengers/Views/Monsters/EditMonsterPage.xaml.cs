using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Views.Monsters;
using GameOfChallengers.Models;

namespace GameOfChallengers.Views.Monsters
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditMonsterPage : ContentPage
	{
        CreatureDetailViewModel _viewModel;
        private CreatureDetailViewModel viewModel;

 //       public EditMonsterPage(MonsterDetailViewModel viewModel)
 //       {
 //BindingContext =  this.viewModel = viewModel;
 //       }
        private bool _NeedsRefresh;

        public Creature Data { get; set; }
       
        public EditMonsterPage(CreatureDetailViewModel viewModel)
        {
           Data = viewModel.Data;
            viewModel.Title = "Edit" + viewModel.Title;

            InitializeComponent();

            BindingContext = this._viewModel = viewModel;
        }
        
        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "UpdateMonster", _viewModel.Data);
            _NeedsRefresh = true;
            await Navigation.PopToRootAsync();
        }
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }



}