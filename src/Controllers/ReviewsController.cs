using AmazonCollectReviews.Model;
using AmazonCollectReviews.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonCollectReviews.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IApiClient _apiClient;
        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(ILogger<ReviewsController> logger, IApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        [HttpGet("{asin}")]
        public IActionResult GetReviewByAsin(string asin)
        {
            var result = _apiClient.FetchClientReviews(asin);
                        
            return new JsonResult(result);
        }
    }
}
