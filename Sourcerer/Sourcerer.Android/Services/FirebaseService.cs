using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;

using Sourcerer.Models;
using Sourcerer.Services;

using Firebase;
using Firebase.Firestore;
using Firebase.Database;
using Java.Lang;

[assembly: Xamarin.Forms.Dependency(typeof(Sourcerer.Droid.Services.FirebaseService))]
namespace Sourcerer.Droid.Services
{
    public class FirebaseService : IFirebaseService<Story>
    {
        // List<IStory> Stories;
        public static List<Story> stories;

        public static DatabaseReference currentStories;
        public static List<Story> currentStoriesList;

        public FirebaseService()
        {
            stories = new List<Story>();
            var db = FirebaseDatabase.GetInstance(FirebaseApp.Instance);
            currentStories = db.GetReference("stories");

            currentStories.AddListenerForSingleValueEvent(new Listener());
            /*
            stories.Add(new Story
            {
                Title = "Test Title",
                Overview = "Overview",
                ImgUrl = "No Url",
                ImgCaption = "Caption",
                Context = null,
                ImportantPoints = null,
                Significance = null,
            });
            stories.Add(new Story
            {
                Title = "Test Title 2",
                Overview = "Overview 2",
                ImgUrl = "No Url",
                ImgCaption = "Caption",
                Context = null,
                ImportantPoints = null,
                Significance = null,
            });
            stories.Add(new Story
            {
                Title = "Test Title 3",
                Overview = "Overview 3",
                ImgUrl = "No Url",
                ImgCaption = "Caption",
                Context = null,
                ImportantPoints = null,
                Significance = null,
            });
            */
        }

        public void Init()
        {
            Console.WriteLine("Init, THIS IS A MUCH LONGER VERSION");

        }

        public async Task<IEnumerable<Story>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(stories);
        }
    }

    public class Listener : Java.Lang.Object, IValueEventListener
    {
        //public new IntPtr Handle => throw new NotImplementedException();

        public new void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void OnCancelled(DatabaseError error)
        {
            //throw new NotImplementedException();
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            foreach (DataSnapshot child in snapshot.Children.ToEnumerable())
            {
                if (child.HasChild("title") && child.HasChild("imgUrl")
                    && child.HasChild("imgCaption") && child.HasChild("overview")
                    && child.HasChild("context") && child.HasChild("importantPoints")
                    && child.HasChild("significance"))
                {
                    StoryObj childStory = (StoryObj)child;
                    if (childStory.ImgUrl != "false")
                        FirebaseService.stories.Add(childStory);
                }
                else
                {
                    //TODO: log this
                    Console.WriteLine("Failed Check");
                }
            }
        }
    }

    class StoryObj : Story
    {
        public static explicit operator StoryObj(DataSnapshot snapshot)
        {
            StoryObj retStory;
            try
            {
                retStory = new StoryObj
                {
                    Title = snapshot.Child("title").Value.ToString(),
                    ImgUrl = snapshot.Child("imgUrl").Value.ToString(),
                    ImgCaption = snapshot.Child("imgCaption").Value.ToString(),
                    Overview = snapshot.Child("overview").Value.ToString(),
                    ImportantPoints = null,
                    Context = null,
                    Significance = null
                    /*
                    Lat = (Double)snapshot.Child("lat").Value,
                    Lng = (Double)snapshot.Child("lng").Value,
                    Name = snapshot.Child("name").Value.ToString(),
                    Title = snapshot.Child("title").Value.ToString()
                    */
                };
                return retStory;
            }
            catch (NullReferenceException e)
            {
                //TODO: log these
                Console.WriteLine(e);
                return new StoryObj { ImgCaption = "false" };
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e);
                return new StoryObj { ImgCaption = "false" };
            }
        }
    }
}