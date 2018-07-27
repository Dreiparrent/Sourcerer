using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Firebase;
using Firebase.Firestore;
using Firebase.Analytics;
using Sourcerer.Droid.Services;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Sourcerer.Droid
{
    [Activity(Label = "Sourcerer", Icon = "@mipmap/logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        FirebaseAnalytics firebaseAnalytics;
        protected override void OnCreate(Bundle bundle)
        {
            // Xamarin.Forms.Platform.Android.
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            // toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            // var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            // SetSupportActionBar(toolbar);
            // SetActionBar(toolbar);
            // ActionBar.Title = "My Title";
            // toolbar.SetTitle(Resource.String.app_name);
            // this.ActionBar.Title = "My Toolbar";

            FirebaseApp.InitializeApp(this);
            firebaseAnalytics = FirebaseAnalytics.GetInstance(this);

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            // var x = typeof(Xamarin.Forms.Themes.DarkThemeResources);
            //  var x = typeof(Xamarin.Forms.Themes.LightThemeResources);
            // x = typeof(Xamarin.Forms.Themes.Android.UnderlineEffect);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
                ToastLength.Short).Show();
            System.Diagnostics.Debug.WriteLine("ICON: {0}", item.Icon);
            return base.OnOptionsItemSelected(item);
        }
    }
}

