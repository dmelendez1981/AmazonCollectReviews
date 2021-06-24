using AmazonCollectReviews.Model;
using System.Collections.Generic;

namespace AmazonCollectReviews.Services
{
    public interface IApiClient
    {
        List<Review> FetchClientReviews(string ansi);
    }
}
