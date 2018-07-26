using System;
using System.Collections.Generic;
using System.Text;

namespace Sourcerer.Models
{
    public class Story
    {
        public string ImgUrl { get; set; }
        public string ImgCaption { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public List<Point> Context { get; set; }
        public List<Point> ImportantPoints { get; set; }
        public List<Point> Significance { get; set; }
    }
}
