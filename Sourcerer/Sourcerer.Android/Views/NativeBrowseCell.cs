using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Sourcerer.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Sourcerer.Droid.Views
{
    internal class NativeBrowseCell : LinearLayout, INativeElementView
    {
        public TextView Title { get; set; }
        public TextView Overview { get; set; }
        public ImageView ImageView { get; set; }

        public BrowseCell NativeCell { get; private set; }
        public Element Element => NativeCell;

        public NativeBrowseCell(Context context, BrowseCell cell) : base(context)
        {
            NativeCell = cell;

            var view = (context as Activity).LayoutInflater.Inflate(Resource.Layout.NativeBrowseCell, null);
            Title = view.FindViewById<TextView>(Resource.Id.Title);
            Overview = view.FindViewById<TextView>(Resource.Id.Overview);
            ImageView = view.FindViewById<ImageView>(Resource.Id.Image);

            AddView(view);
        }

        public void UpdateCell(BrowseCell cell)
        {
            Title.Text = cell.Title;
            Overview.Text = cell.Overview;

            // Dispose of the old image
            if (ImageView.Drawable != null)
            {
                using (var image = ImageView.Drawable as BitmapDrawable)
                {
                    if (image != null)
                    {
                        if (image.Bitmap != null)
                        {
                            image.Bitmap.Dispose();
                        }
                    }
                }
            }

            SetImage(cell.ImgUrl);
        }

        public void SetImage(string filename)
        {
            if (!string.IsNullOrWhiteSpace(filename))
            {
                // Display new image
                Context.Resources.GetBitmapAsync(filename).ContinueWith((t) =>
                {
                    var bitmap = t.Result;
                    if (bitmap != null)
                    {
                        ImageView.SetImageBitmap(bitmap);
                        bitmap.Dispose();
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                // Clear the image
                ImageView.SetImageBitmap(null);
            }
        }
    }
}