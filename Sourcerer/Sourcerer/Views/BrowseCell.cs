using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Sourcerer.Views
{
    public class BrowseCell : ViewCell
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(BrowseCell), "");

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty OverviewProperty =
            BindableProperty.Create("Overview", typeof(string), typeof(BrowseCell), "");

        public string Overview
        {
            get { return (string)GetValue(OverviewProperty); }
            set { SetValue(OverviewProperty, value); }
        }

        public static readonly BindableProperty ImgUrlProperty =
            BindableProperty.Create("ImgUrl", typeof(string), typeof(BrowseCell), "");

        public string ImgUrl
        {
            get { return (string)GetValue(ImgUrlProperty); }
            set { SetValue(ImgUrlProperty, value); }
        }
    }
}
