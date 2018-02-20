using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GameOfChallengers.Models;
using GameOfChallengers.ViewModels;

namespace GameOfChallengers.Views.Character
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CharacterDetailPage : ContentPage
	{
        private CreatureDetailViewModel _viewModel;
        public Creature Data { get; set; }

        public CharacterDetailPage(CreatureDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        public CharacterDetailPage()
        {
            InitializeComponent();
            Data = new Creature
            {
                Name = "Character name",
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
            await Navigation.PushAsync(new EditCharacterPage(_viewModel));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new DeleteCharacterPage(_viewModel));

        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}