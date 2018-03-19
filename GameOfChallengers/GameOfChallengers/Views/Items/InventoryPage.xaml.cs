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
        String message;
        NewCharacter newcPage;
        EditCharacter editPage;

        public InventoryPage()
        {
            
        }

        public InventoryPage(NewCharacter page)//(Creature Data)
        {
           
            InitializeComponent();
            BindingContext = _viewModel = ItemsViewModel.Instance;
            newcPage = page;
            character = page.Data; 
        }


        public InventoryPage(EditCharacter page)//(Creature Data)
        {
            InitializeComponent();
            BindingContext = _viewModel = ItemsViewModel.Instance;
            editPage = page;
            character = page.Data;
        }


        private void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Item;
            if (data == null)
                return;

            //  character.AddItem(data.Location, data.Id);
            ItemsSelected.Add(data);
            message = message + data.Name + "  added" + "\n";
            AddedItem.Text = message;
        }


        private async void SaveItems_Clicked(object sender, EventArgs e)
        {
            if (ItemsSelected == null)
            {
                await Navigation.PopAsync();
            }
            else
            {
                if (newcPage == null)
                {
                    editPage.SaveItemAsync(ItemsSelected);
                }
                else
                {
                    newcPage.SaveItem(ItemsSelected);
                }

                Navigation.RemovePage(this);
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
