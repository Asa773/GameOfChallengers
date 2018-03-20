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
        private ItemDetailViewModel _viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var data = new Item();

            _viewModel = new ItemDetailViewModel(data);
            BindingContext = _viewModel;
        }


        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditItemPage(_viewModel)); //Edit item page will be opened when clicked
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeleteItemPage(_viewModel));//delete item page will be opened when clicked
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();//goes back to the items detail page
        }
    }
}
