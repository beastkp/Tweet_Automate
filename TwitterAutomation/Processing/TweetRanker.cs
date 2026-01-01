using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterDiscovery.Models;

namespace TwitterDiscovery.Processing
{
    public static class TweetRanker
    {
        public static IEnumerable<(TweetDto Tweet, double Score)> Rank(IEnumerable<TweetDto> tweets)
        {
            return tweets.Select(t =>
            {
                var ageHours = (DateTime.UtcNow - t.CreatedAt).TotalHours;

                var score =
                    (t.Metrics.LikeCount * 2) +
                    (t.Metrics.ReplyCount * 3) -
                    ageHours;

                return (t, score);
            })
            .OrderByDescending(x => x.score);
        }
    }

}
