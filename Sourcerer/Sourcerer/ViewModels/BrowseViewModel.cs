using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Sourcerer.Models;
using Sourcerer.Views;

namespace Sourcerer.ViewModels
{
    public class BrowseViewModel : BaseViewModel
    {
        public ObservableCollection<Story> Stories { get; set; }
        public Command LoadStoriesCommand { get; set; }

        public BrowseViewModel()
        {
            Title = "Browse";
            Stories = new ObservableCollection<Story>();
            LoadStoriesCommand = new Command(async () => await ExecuteLoadStoriesCommand());

            /*
            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Item;
                Items.Add(_item);
                await DataStore.AddItemAsync(_item);
            });
            */
        }

        async Task ExecuteLoadStoriesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Stories.Clear();
                var stories = await FirebaseService.GetItemsAsync(true);
                foreach (var story in stories)
                {
                    Stories.Add(story);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}