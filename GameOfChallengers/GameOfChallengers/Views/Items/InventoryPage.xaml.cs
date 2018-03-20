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
            // Set the data binding for the page
            BindingContext = _viewModel = ItemsViewModel.Instance;
            newcPage = page; //page to assign items in new character page
            character = page.Data; 
        }


        public InventoryPage(EditCharacter page)//(Creature Data)
        {
            InitializeComponent();
            // Set the data binding for the page
            BindingContext = _viewModel = ItemsViewModel.Instance;
            editPage = page;//page to assign items in edit character page
            character = page.Data;
        }


        private void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Item;
            if (data == null)
                return;

            //  character.AddItem(data.Location, data.Id);
            ItemsSelected.Add(data); //when items are selected they are added
            message = message + data.Name + "  added" + "\n";
            AddedItem.Text = message; 
        }


        private async void SaveItems_Clicked(object sender, EventArgs e)
        {
            if (ItemsSelected == null)
            {
                await Navigation.PopAsync();//If no items are selected then sent back to the edit/new character page
            }
            else
            {
                if (newcPage == null)
                {
                    editPage.SaveItemAsync(ItemsSelected);// the items will be saved to the edit character page 
                }
                else
                {
                    newcPage.SaveItem(ItemsSelected);// the items will be saved to the new character page 
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
