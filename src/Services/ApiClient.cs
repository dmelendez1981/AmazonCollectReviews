using AmazonCollectReviews.Configuration;
using AmazonCollectReviews.Model;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace AmazonCollectReviews.Services
{
    public class ApiClient : IApiClient
    {
        private readonly ServiceSettings _settings;
        private readonly ILogger<ApiClient> _logger;

        // List of invalid http status code
        private static readonly List<HttpStatusCode> invalidStatusCode = new List<HttpStatusCode> {
            HttpStatusCode.BadRequest,
            HttpStatusCode.BadGateway,
            HttpStatusCode.InternalServerError,
            HttpStatusCode.RequestTimeout,
            HttpStatusCode.Forbidden,
            HttpStatusCode.GatewayTimeout
        };

        public ApiClient(ILogger<ApiClient> logger, IOptions<ServiceSettings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
        }

        public List<Review> FetchClientReviews(string ansi)
        {
            //Adding Polly policies
            var retryPolicy = Policy
             .HandleResult<IRestResponse>(resp => invalidStatusCode.Contains(resp.StatusCode))
             .WaitAndRetry(10, i => TimeSpan.FromSeconds(Math.Pow(2, i)), (result, timeSpan, currentRetryCount, context) =>
             {
                 _logger.LogError($"Request failed with {result.Result.StatusCode}. Waiting {timeSpan} before next retry. Retry attempt {currentRetryCount}");
             });

            var client = new RestClient($"{_settings.AmazonProductReviewsUrl + ansi}");

            // Initiated the Rest Requeste
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;

            var policyResponse = retryPolicy.ExecuteAndCapture(() =>
            {
                return client.Get(request);
            });

            if (policyResponse.Result.StatusCode != HttpStatusCode.NotFound)
            {
                //store the html of the page in a variable
                var doc = new HtmlDocument();
                doc.LoadHtml(policyResponse.Result.Content);

                var reviews = doc.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Equals("a-section celwidget")).ToList();

                List<Review> listOfReviews = new List<Review>();

                //to display each product review
                foreach (var htmlNode in reviews)
                {
                    listOfReviews.Add(getProductReviewFromAmazonData(htmlNode.ChildNodes, ansi));
                }
                return listOfReviews;
            }
            else
            {
                return null;
            }
        }

        private Review getProductReviewFromAmazonData(HtmlNodeCollection nodes, string asin)
        {
            Review review = new Review();
            review.Asin = asin;
            if (nodes[1] != null)
            {
                review.ReviewRating = nodes[1].InnerText.Substring(0, 18);
                review.ReviewTitle = nodes[1].InnerText.Substring(18, (nodes[1].InnerText.Length - 18))
                    .Replace("\n", "").Trim();
            }
            if(nodes[2] != null)
            {
                review.ReviewDate = nodes[2].InnerText;
            }
            if(nodes[4] != null)
            {
                review.ReviewContent = nodes[4].InnerText.Replace("\n", "").Trim();
            }            

            return review;
        }
    }
}
