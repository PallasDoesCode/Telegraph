using System.Collections.Generic;

namespace CoinMarketCap.Core.Models
{
    public class Listings
    {
        public IEnumerable<Listing> Data { get; set; }
        public Metadata Metadata { get; set; }
    }
}
