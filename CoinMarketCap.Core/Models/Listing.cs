namespace CoinMarketCap.Core.Models
{
    public class Listing
    {
        #region Properties

        public long Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string WebsiteSlug { get; set; }

        #endregion
    }
}