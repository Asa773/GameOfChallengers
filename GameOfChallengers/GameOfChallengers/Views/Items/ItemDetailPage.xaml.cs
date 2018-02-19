using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GameOfChallengers.Models;
using GameOfChallengers.ViewModels;

namespace GameOfChallengers.Views.Items
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private ItemDetailViewModel _viewModel;
        public Item Data { get; set; }

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var data = new Item
            {
                Name = "Item1",
                //Description = "This is an item description."
            };

            _viewModel = new ItemDetailViewModel(data);
            BindingContext = _viewModel;
        }


        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditItemPage(_viewModel));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete", "Are You sure, you want to Delete this?", "Yes", "No");
            if (answer)
            {
                MessagingCenter.Send(this, "DeleteData", Data);

                // Remove Item Details Page manualy
                //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

                await Navigation.PopAsync();
            }
        }

    }
}
