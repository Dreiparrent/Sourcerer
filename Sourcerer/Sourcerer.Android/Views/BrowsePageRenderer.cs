using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using Sourcerer.Views;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;

[assembly: ExportRenderer(typeof(Sourcerer.Views.BrowsePage1), typeof(Sourcerer.Droid.Views.BrowsePageRenderer))]
namespace Sourcerer.Droid.Views
{
    class BrowsePageRenderer : PageRenderer
    {
        Context Ccontext;
        AppCompatActivity Cactivity;
        // Android.Support.V7.Widget.Toolbar Ctoolbar;
        public BrowsePageRenderer(Context context) : base(context)
        {
            System.Diagnostics.Debug.WriteLine("WORKING");
            Ccontext = context;
            Cactivity = (AppCompatActivity)context;
            SetMenu();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            SetMenu();
            if (e.OldElement != null || Element == null)
            {
                return;
            }
        }
        
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            if (changed)
            {
                SetMenu();
            }
            System.Diagnostics.Debug.WriteLine("Changed: {0}", changed);
        }

        private void SetMenu()
        {
            var Ctoolbar = Cactivity.FindViewById<Toolbar>(Resource.Id.toolbar);
            // Ctoolbar.Title = "Test Title1";
            Cactivity.SetSupportActionBar(Ctoolbar);
            // Cactivity.SupportActionBar.Title = "Test Title";
        }
    }
}