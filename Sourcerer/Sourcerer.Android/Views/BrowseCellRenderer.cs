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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Support.V7;
using Sourcerer.Views;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(Sourcerer.Views.BrowseCell), typeof(Sourcerer.Droid.Views.BrowseCellRenderer))]
namespace Sourcerer.Droid.Views
{
    class BrowseCellRenderer : ViewCellRenderer
    {
        NativeBrowseCell cell;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            var nativeCell = (BrowseCell)item;
            Console.WriteLine("\t\t" + nativeCell.Title);

            cell = convertView as NativeBrowseCell;
            if (cell == null)
            {
                cell = new NativeBrowseCell(context, nativeCell);
            }
            else
            {
                cell.NativeCell.PropertyChanged -= OnNativeCellPropertyChanged;
            }

            nativeCell.PropertyChanged += OnNativeCellPropertyChanged;

            cell.UpdateCell(nativeCell);
            return cell;
        }

        void OnNativeCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var nativeCell = (BrowseCell)sender;
            if (e.PropertyName == BrowseCell.TitleProperty.PropertyName)
            {
                cell.Title.Text = nativeCell.Title;
            }
            else if (e.PropertyName == BrowseCell.OverviewProperty.PropertyName)
            {
                cell.Overview.Text = nativeCell.Overview;
            }
            else if (e.PropertyName == BrowseCell.ImgUrlProperty.PropertyName)
            {
                cell.SetImage(nativeCell.ImgUrl);
            }
        }

    }
}