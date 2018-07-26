using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ListView), typeof(Sourcerer.iOS.Views.CustomListViewRenderer))]
namespace Sourcerer.iOS.Views
{
    public class CustomListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            ((UITableViewController)ViewController).RefreshControl.TintColor = UIColor.Red;

        }
    }
}