using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using Sourcerer.Models;
using Sourcerer.Services;

using Firebase;
using Firebase.Database;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(Sourcerer.iOS.Services.FirebaseService))]
namespace Sourcerer.iOS.Services
{
    public class FirebaseService : IFirebaseService<Story>
    {
        public static List<Story> stories;

        public static Firebase.Core.App app;
        public static DatabaseReference dataRef;
        public static Dictionary<String, String> _salesList;

        public FirebaseService()
        {
            stories = new List<Story>();

            Database db = Database.From(Firebase.Core.App.DefaultInstance);
            dataRef = db.GetRootReference();

            DatabaseReference testDir = dataRef.GetChild("currentSales");
            nuint handleReference = testDir.ObserveEvent(DataEventType.Value, (snapshot) => {
                NSEnumerator children = snapshot.Children;

                var child = children.NextObject() as DataSnapshot;

                while (child != null)
                {
                    var data = (StoryObj)child.GetValue<NSDictionary>();
                    if (data.ImgUrl != "false")
                        stories.Add(data);

                    child = children.NextObject() as DataSnapshot;
                }
            }, (error) =>
            {
                Console.WriteLine(error);
            });
        }

        public async Task<IEnumerable<Story>> GetItemsAsync(bool forceRefresh = false)
        {
            // throw new NotImplementedException();
            return await Task.FromResult(stories);
        }

        public void Init()
        {
            Console.WriteLine("Init");
        }
    }

    class StoryObj : Story
    {
        public static explicit operator StoryObj(NSDictionary dictionary)
        {
            try
            {
                var retSale = new StoryObj
                {
                    ImgUrl = (NSString)dictionary["begin"].ToString(),
                    ImgCaption = (NSString)dictionary["desc"].ToString(),
                    Overview = (NSString)dictionary["end"].ToString(),
                    /*
                    Lat = (double)(NSNumber)dictionary["lat"],
                    Lng = (double)(NSNumber)dictionary["lng"],
                    Name = (NSString)dictionary["name"].ToString(),
                    Title = (NSString)dictionary["title"].ToString()
                    */
                };
                return retSale;
            }
            catch (NullReferenceException e)
            {
                //TODO: firebase log these
                Console.WriteLine($"MY ERROR: {e}");
                return new StoryObj { ImgUrl = "false" };
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine($"MY ERROR: {e}");
                return new StoryObj { ImgUrl = "false" };
            }
        }
    }
}