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
        private CreatureDetailViewModel _viewModel;

        public Creature Data { get; set; }
        String selectedImg;
        public EditMonsterPage(CreatureDetailViewModel viewModel)
        {
            Data = viewModel.Data;
            viewModel.Title = "Edit" + viewModel.Title;
            selectedImg = Data.ImageURI;
            InitializeComponent();


            BindingContext = _viewModel = viewModel;
            selectedImg = ImagePicker.SelectedItem.ToString();
            Data.ImageURI = ImagePicker.SelectedItem.ToString();
        }


        private void setImage(object sender, EventArgs e)
        {
            selectedImg = ImagePicker.SelectedItem.ToString();
            ChangeImg.Source = selectedImg;
        }

        //Saves the edited monster after creating it and goes back to the monsters page
        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "EditData", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new MonsterDetailPage(new CreatureDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }



}