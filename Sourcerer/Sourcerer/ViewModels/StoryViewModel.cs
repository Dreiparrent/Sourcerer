using System;

using Sourcerer.Models;

namespace Sourcerer.ViewModels
{
    public class StoryViewModel : BaseViewModel
    {
        public Story Story { get; set; }
        public StoryViewModel(Story story = null)
        {
            Title = story?.Title;
            Story = story;
        }
    }
}
