using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterDiscovery.Models
{
    public class TweetDto
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public PublicMetrics Metrics { get; set; }
    }

    public class PublicMetrics
    {
        public int LikeCount { get; set; }
        public int ReplyCount { get; set; }
        public int RetweetCount { get; set; }
    }
}
