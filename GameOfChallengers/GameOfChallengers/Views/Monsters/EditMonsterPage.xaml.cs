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
         MonsterDetailViewModel _viewModel;
        private MonsterDetailViewModel viewModel;

 //       public EditMonsterPage(MonsterDetailViewModel viewModel)
 //       {
 //BindingContext =  this.viewModel = viewModel;
 //       }
        private bool _NeedsRefresh;

        public Creature Data { get; set; }
       
        public EditMonsterPage(MonsterDetailViewModel viewModel)
        {
           Data = viewModel.Data;
            viewModel.Title = "Edit" + viewModel.Title;

            InitializeComponent();

            BindingContext = this._viewModel = viewModel;
        }
        private void InitializeComponent()
        {
            throw new NotImplementedException();
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