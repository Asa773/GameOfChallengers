using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GameOfChallengers.Models;
using GameOfChallengers.Views.Items;

namespace GameOfChallengers.Views.Character
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCharacter : ContentPage
    {
        public Creature Data { get; set; }
        String selectedImg = "BlankImage.jpeg" ; //set to the blank before setting a new one

        // Constructor for the page, will create a new blank character
        public NewCharacter()
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
                ImageURI ="BlankImage.jpeg",

            };
           
            BindingContext = this;
            //To the select the images while editing the character
            selectedImg = ImagePicker.SelectedItem.ToString();
           // ChangeImg.Source = selectedImg;
            Data.ImageURI = ImagePicker.SelectedItem.ToString();
        }

        //Sets the image as per the selected image by the player
        private void setImage(object sender, EventArgs e)
        {
            selectedImg = ImagePicker.SelectedItem.ToString();
            ChangeImg.Source = selectedImg;
        }

        // Respond to the Save click
        // Send the add message to so it gets added...
        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddData", Data);
            await Navigation.PopAsync();
        }

        //Save the selected assigned items to the character
        public void SaveItem(List<Item> ItemsList)
        {
            foreach (var item in ItemsList)
            {
                if (item.Location == ItemLocationEnum.Finger)//Check if the finger item should be assigned to right or left finger 
                {
                    if (Data.RightFinger == null)
                    {
                        item.Location = ItemLocationEnum.RightFinger;
                    }
                    else
                    {
                        item.Location = ItemLocationEnum.LeftFinger;
                    }

                }
                Data.AddItem(item.Location, item.Id);//Add that item to the character depending on the location of the item selected
            }
        }

        // Cancel and go back a page in the navigation stack
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void AddItems_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InventoryPage(this));//takes to the inventory page when add items is clicked
        }
    }
}