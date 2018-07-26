using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ListView), typeof(Sourcerer.Droid.Views.CustomListViewRenderer))]
namespace Sourcerer.Droid.Views
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class CustomListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                FieldInfo[] fields = typeof(ListViewRenderer).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                var refresh = (SwipeRefreshLayout)fields.First(x => x.Name == "_refresh").GetValue(this);
                int[] tmpColors = new int[] { 2131361877, 2130772144 };
                refresh.SetColorSchemeColors(tmpColors);
            }
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}