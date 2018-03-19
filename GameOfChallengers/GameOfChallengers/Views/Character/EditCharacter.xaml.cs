using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GameOfChallengers.Models;
using GameOfChallengers.ViewModels;
using GameOfChallengers.Views.Items;

namespace GameOfChallengers.Views.Character
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCharacter : ContentPage
    {
        private CreatureDetailViewModel _viewModel;

        public Creature Data { get; set; }
        String selectedImg;
        public EditCharacter(CreatureDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            selectedImg = Data.ImageURI ; 
            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
            selectedImg = ImagePicker.SelectedItem.ToString();
            Data.ImageURI = ImagePicker.SelectedItem.ToString();

        }


        private void setImage(object sender, EventArgs e)
        {
            selectedImg = ImagePicker.SelectedItem.ToString();
            ChangeImg.Source = selectedImg;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "EditData", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new CharacterDetail(new CreatureDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }


        public void SaveItem(List<Item> ItemsList)
        {
            foreach (var item in ItemsList)
            {
                if (item.Location == ItemLocationEnum.Finger)
                {
                    item.Location = ItemLocationEnum.RightFinger;
                }
                Data.AddItem(item.Location, item.Id);
            }
        }


        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void AddItems_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InventoryPage(this));
        }
    }
}