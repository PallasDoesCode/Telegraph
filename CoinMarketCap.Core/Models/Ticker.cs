using System.Collections.Generic;

namespace CoinMarketCap.Core.Models
{
    public class Ticker
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string WebsiteSlug { get; set; }
        public long Rank { get; set; }
        public decimal CirculatingSupply { get; set; }
        public decimal TotalSupply { get; set; }
        public decimal MaxSupply { get; set; }
        public Quotes Quotes { get; set; }
        public string LastUpdated { get; set; }
    }
}
