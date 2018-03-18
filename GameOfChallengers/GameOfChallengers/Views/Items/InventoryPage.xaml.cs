using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GameOfChallengers.Models;
using GameOfChallengers.ViewModels;
using System.Collections.Generic;
using GameOfChallengers.Views.Character;
using System;

namespace GameOfChallengers.Views.Items
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventoryPage : ContentPage
    {
        private Creature character;
        private ItemsViewModel _viewModel;
        public List<Item> ItemsSelected = new List<Item>();
        NewCharacter newcPage;

        public InventoryPage()
        {
            //InitializeComponent();
            //BindingContext = _viewModel = ItemsViewModel.Instance;
        }

        public InventoryPage(NewCharacter page)//(Creature Data)
        {
            InitializeComponent();
            BindingContext = _viewModel = ItemsViewModel.Instance;
            newcPage = page;
            character = page.Data;
        }


        private void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Item;
            if (data == null)
                return;

            //  character.AddItem(data.Location, data.Id);
            ItemsSelected.Add(data);
           
           // await Navigation.PopAsync();
           // await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(data)));

            // Manually deselect item.
           // ItemsListView.SelectedItem = null;
        }


        private async void SaveItems_Clicked(object sender, EventArgs e)
        {
            if (ItemsSelected == null)
            {
                 await Navigation.PopAsync();
            }
            else
            {

                newcPage.SaveItem(ItemsSelected);
                await Navigation.PopAsync();
            }

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
