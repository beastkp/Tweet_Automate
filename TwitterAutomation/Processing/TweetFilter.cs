using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterDiscovery.Models;

namespace TwitterDiscovery.Processing
{
    public static class TweetFilter
    {
        public static IEnumerable<TweetDto> Apply(IEnumerable<TweetDto> tweets)
        {
            return tweets.Where(t =>
                t.Metrics.LikeCount >= 1 &&
                (DateTime.UtcNow - t.CreatedAt).TotalHours <= 72 &&
                !string.IsNullOrWhiteSpace(t.Text)
            );
        }
    }
}
