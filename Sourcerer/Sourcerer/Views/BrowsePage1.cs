using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Sourcerer.ViewModels;
using Sourcerer.Models;
using System.Diagnostics;


#if __ANDROID__
using Xamarin.Forms.Platform.Android;
using Sourcerer.Droid;
using Android.Views;
using Android.Support.V7.Widget;
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
        ListView listView;
        public BrowsePage1()
        {
            
            viewModel = new BrowseViewModel();
            /*
            listView = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                ItemsSource = viewModel.Stories,
                ItemTemplate = new DataTemplate(() =>
                {
                    var nativeCell = new NativeCell();
                    nativeCell.SetBinding(NativeCell.NameProperty, "Title");
                    nativeCell.SetBinding(NativeCell.CategoryProperty, "Overview");
                    nativeCell.SetBinding(NativeCell.ImageFilenameProperty, "ImgUrl");

                    return nativeCell;
                }),
                RefreshCommand = viewModel.LoadStoriesCommand,
                IsPullToRefreshEnabled = true,


            };
#pragma warning disable CS0612 // Type or member is obsolete
            listView.SetBinding<BrowseViewModel>(ListView.IsRefreshingProperty, vm => vm.IsBusy, mode: BindingMode.OneWay);
#pragma warning restore CS0612 // Type or member is obsolete

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    Padding = new Thickness(0, 20, 0, 0);
                    break;
                case Device.Android:
                case Device.UWP:
                    Padding = new Thickness(0);
                    break;
            }

            Content = new StackLayout
            {
                Children = {
                    listView
                }
            };
            */
            
            StoriesListView = new ListView
            {
                ItemsSource = viewModel.Stories,
                BindingContext = viewModel,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HasUnevenRows = true,
                RefreshCommand = viewModel.LoadStoriesCommand,
                IsPullToRefreshEnabled = true,
                // IsRefreshing = viewModel.IsRefreshing,
                // ItemTemplate = new DataTemplate(typeof(BrowseCell)),
                ItemTemplate = new DataTemplate(() =>
                {
                    BrowseCell nativeCell = new BrowseCell();
                    nativeCell.SetBinding(BrowseCell.TitleProperty, "Title");
                    nativeCell.SetBinding(BrowseCell.OverviewProperty, "Overview");
                    nativeCell.SetBinding(BrowseCell.ImgUrlProperty, "ImgUrl");

                    return nativeCell;
                })
            };
#pragma warning disable CS0612 // Type or member is obsolete
            StoriesListView.SetBinding<BrowseViewModel>(ListView.IsRefreshingProperty, vm => vm.IsBusy, mode: BindingMode.OneWay);
#pragma warning restore CS0612 // Type or member is obsolete

            StoriesListView.ItemSelected += OnStorySelected;
            Content = new StackLayout
            {
                Children = {
                    StoriesListView,
					// new Label { Text = "Welcome to Xamarin.Forms!" }
				}
            };
#if __ANDROID__
            Title = "Browse";
#endif
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