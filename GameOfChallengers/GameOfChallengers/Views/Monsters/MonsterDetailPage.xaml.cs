using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GameOfChallengers.Models;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Views.Monsters;
using GameOfChallengers.Views.Monster;

namespace GameOfChallengers.Views.Monsters
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MonsterDetailPage : ContentPage
	{
        MonsterDetailViewModel _viewModel;

        public MonsterDetailPage(MonsterDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }
        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        public MonsterDetailPage()
        {
            InitializeComponent();

            var data = new Creature
            {
                Name = "New Monster",
                CurrHealth = 0,
                Attack = 0,
                Level = 0,
                Speed = 0

                
            };

            _viewModel = new MonsterDetailViewModel(data);
            BindingContext = _viewModel;
        }

        async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditMonsterPage(_viewModel));
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeleteMonsterPage(_viewModel));
        }
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


    }




}