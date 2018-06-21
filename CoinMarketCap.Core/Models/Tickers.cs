using System.Collections.Generic;

namespace CoinMarketCap.Core.Models
{
    public class Tickers
    {
        public Ticker[] Data { get; set; }
        public Metadata Metadata { get; set; }
    }
}
