using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using TwitterDiscovery.Models;
using TwitterDiscovery.Processing;
using TwitterDiscovery.Procurement;

namespace TwitterAutomation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly TwitterClient _twitterClient;
        private readonly IMemoryCache _cache;
        public List<(TweetDto Tweet, double Score)> Tweets { get; private set; } = [];

        public IndexModel(ILogger<IndexModel> logger, TwitterClient twitterClient,IMemoryCache cache)
        {
            _logger = logger;
            _twitterClient = twitterClient;
            _cache = cache;
        }

        public async Task OnGetAsync()
        {
            const string cacheKey = "daily_tweets";

            if (!_cache.TryGetValue(cacheKey, out List<(TweetDto Tweet, double Score)> cached))
            {
                var rawTweets = await _twitterClient.SearchTweetsAsync();
                var filtered = TweetFilter.Apply(rawTweets);
                cached = TweetRanker.Rank(filtered).Take(50).ToList();

                _cache.Set(cacheKey, cached, TimeSpan.FromHours(12));
            }

            Tweets = cached;
        }
    }
}
