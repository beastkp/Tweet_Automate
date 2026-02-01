using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwitterDiscovery.Models;

namespace TwitterDiscovery.Procurement
{
    public class TwitterClient
    {
        private readonly HttpClient _httpClient;

        public TwitterClient(string bearerToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", bearerToken);
        }

        public async Task<List<TweetDto>> SearchTweetsAsync()
        {
            var query =
                "(" +
                    "\"as a developer\" OR \"as a software engineer\" OR \"as a programmer\" " +
                    "OR \"developer life\" OR \"dev life\" OR \"software engineering\"" +
                ") " +
                "(" +
                    "\"burnout\" OR \"stuck\" OR \"frustrating\" OR \"hard\" OR \"confusing\" " +
                    "OR \"learning to code\" OR \"learning programming\" " +
                    "OR \"hot take\" OR \"unpopular opinion\"" +
                ") " +
                "-is:retweet -is:reply lang:en";



            var url =
                "https://api.twitter.com/2/tweets/search/recent" +
                $"?query={Uri.EscapeDataString(query)}" +
                "&max_results=10" +
                "&tweet.fields=created_at,public_metrics";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Twitter API error: {error}");
            }

            using var stream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);

            var tweets = new List<TweetDto>();

            if (!doc.RootElement.TryGetProperty("data", out var data))
                return tweets;

            foreach (var t in data.EnumerateArray())
            {
                tweets.Add(new TweetDto
                {
                    Id = t.GetProperty("id").GetString(),
                    Text = t.GetProperty("text").GetString(),
                    CreatedAt = t.GetProperty("created_at").GetDateTime(),
                    Metrics = new PublicMetrics
                    {
                        LikeCount = t.GetProperty("public_metrics").GetProperty("like_count").GetInt32(),
                        ReplyCount = t.GetProperty("public_metrics").GetProperty("reply_count").GetInt32(),
                        RetweetCount = t.GetProperty("public_metrics").GetProperty("retweet_count").GetInt32()
                    }
                });
            }

            return tweets;
        }
    }
}
