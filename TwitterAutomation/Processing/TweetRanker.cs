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
            return tweets
                .Select(t =>
                {
                    var ageHours = (DateTime.UtcNow - t.CreatedAt).TotalHours;
                    var cappedAge = Math.Min(ageHours, 24);
                    var agePenalty = Math.Log(cappedAge + 1);

                    var score =
                        (t.Metrics.LikeCount * 1.5) +
                        (t.Metrics.ReplyCount * 4) -
                        agePenalty;

                    return (Tweet: t, Score: score);
                })
                .OrderByDescending(x => x.Score);
        }
    }

}
