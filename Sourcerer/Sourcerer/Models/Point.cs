using System;
using System.Collections.Generic;
using System.Text;

namespace Sourcerer.Models
{
    public class Point
    {
        public int Flag { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public class CaptionData {
            public static string Title { get; set; }
            public static string Name { get; set; }
            public static string Date { get; set; }
            public static string Origin { get; set; }
            public static string Url { get; set; }
        };
        public string Caption
        {
            get
            {
                return CaptionData.Title;
            }
        }
    }
}
 