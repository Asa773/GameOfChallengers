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
	public partial class MonsterPage : ContentPage
	{
        private MonstersViewModel _viewModel;

        public MonsterPage()
        {
            InitializeComponent();
            // Set the data binding for the page
            BindingContext = _viewModel = MonstersViewModel.Instance;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Creature;//action to select the monster
            if (data == null)
            {
                return;
            }

            await Navigation.PushAsync(new MonsterDetailPage(new CreatureDetailViewModel (data)));//Goes to the monster Details page

            // Manually deselect item.
            MonstersListView.SelectedItem = null;
        }

        //New monster page will be opened when add is clicked
        private async void AddMonster_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateMonster());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.RemoveAt(0);
            }

            InitializeComponent();

            if (_viewModel.Dataset.Count == 0)
            {
                _viewModel.LoadDataCommand.Execute(null);
            }
            else if (_viewModel.NeedsRefresh())
            {
                _viewModel.LoadDataCommand.Execute(null);
            }

            BindingContext = _viewModel;
        }

    }

}