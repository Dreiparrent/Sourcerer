using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Sourcerer.Models;
using Sourcerer.ViewModels;

namespace Sourcerer.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StoryPage : ContentPage
	{
        StoryViewModel viewModel;

        public StoryPage(StoryViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public StoryPage()
        {
            InitializeComponent();

            var story = new Story
            {
                Title = "Item 1",
                Overview = "This is an item description."
            };

            viewModel = new StoryViewModel(story);
            BindingContext = viewModel;
        }
    }
}