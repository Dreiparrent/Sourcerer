using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Sourcerer.Models;
using Sourcerer.Views;
using Sourcerer.ViewModels;

namespace Sourcerer.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BrowsePage : ContentPage
	{
        BrowseViewModel viewModel;

        public BrowsePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new BrowseViewModel();
        }

        async void OnStorySelected(object sender, SelectedItemChangedEventArgs args)
        {
            var story = args.SelectedItem as Story;
            if (story == null)
                return;

            await Navigation.PushAsync(new StoryPage(new StoryViewModel(story)));

            // Manually deselect item.
            StoriesListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Stories.Count == 0)
                viewModel.LoadStoriesCommand.Execute(null);
        }
    }
}