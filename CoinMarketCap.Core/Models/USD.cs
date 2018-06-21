using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMarketCap.Core.Models
{
    public class USD
    {
        public decimal Price { get; set; }
        public decimal Volume24Hours { get; set; }
        public decimal MarketCap { get; set; }
        public decimal PercentChange1Hour { get; set; }
        public decimal PercentChange24Hours { get; set; }
        public decimal PercentChange7Days { get; set; }
    }
}
