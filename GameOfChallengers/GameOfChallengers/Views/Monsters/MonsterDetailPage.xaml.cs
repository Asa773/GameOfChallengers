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

namespace GameOfChallengers.Views.Monsters
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MonsterDetailPage : ContentPage
	{
        private CreatureDetailViewModel _viewModel;
        public Creature Data { get; set; }

        public MonsterDetailPage(CreatureDetailViewModel viewModel)
        {
            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

        public MonsterDetailPage()
        {
            InitializeComponent();

            Data = new Creature
            {
                //Setting default attributes
                Name = "Monster name",
                Id = Guid.NewGuid().ToString(),
                Type = 0,
                Level = 1,
                XP = 0,
                MaxHealth = 10,
                CurrHealth = 10,
                Alive = true,

            };

            _viewModel = new CreatureDetailViewModel(Data);
            BindingContext = _viewModel;
        }

        
        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditMonsterPage(_viewModel));//Editing the character page
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new DeleteMonsterPage(_viewModel));//deleting the character page

        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();//goes back to the previous page
        }

    }




}