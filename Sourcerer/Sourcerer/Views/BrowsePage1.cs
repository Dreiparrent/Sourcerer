using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Sourcerer.ViewModels;
using Sourcerer.Models;
using Android.Support.V7.Widget;


#if __ANDROID__
using Xamarin.Forms.Platform.Android;
using Sourcerer.Droid;
using Android.Views;
#endif
#if __IOS__
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;
#endif

namespace Sourcerer.Views
{
	public class BrowsePage1 : ContentPage
	{
        BrowseViewModel viewModel;
        ListView StoriesListView;
        public BrowsePage1()
        {
            viewModel = new BrowseViewModel();
            StoriesListView = new ListView
            {
                ItemsSource = viewModel.Stories,
                BindingContext = viewModel,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HasUnevenRows = true,
                RefreshCommand = viewModel.LoadStoriesCommand,
                IsPullToRefreshEnabled = true,
                // IsRefreshing = viewModel.IsRefreshing,
                ItemTemplate = new DataTemplate(typeof(BrowseCell)),
            };
#pragma warning disable CS0612 // Type or member is obsolete
            StoriesListView.SetBinding<BrowseViewModel>(ListView.IsRefreshingProperty, vm => vm.IsBusy, mode: BindingMode.OneWay);
#pragma warning restore CS0612 // Type or member is obsolete

            StoriesListView.ItemSelected += OnStorySelected;
            Content = new StackLayout {
                Children = {
                    StoriesListView,
					// new Label { Text = "Welcome to Xamarin.Forms!" }
				}
			};
            /*
#if __ANDROID__
            PopupMenu popupMenu = new PopupMenu(, null);
            popupMenu.MenuInflater.Inflate(Resource.Id.action_search);
#endif
#if __IOS__
            var testVar = new UIPopoverController
            {
                
            }
#endif
            */
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