using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonCollectReviews.Model
{
    public class Review
    {
        public string Asin { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewContent { get; set; }
        public string ReviewDate { get; set; }
        public string ReviewRating { get; set; }
    }
}
