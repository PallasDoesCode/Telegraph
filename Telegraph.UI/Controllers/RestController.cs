using System.Threading.Tasks;
using CoinMarketCap.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Telegraph.UI.Controllers {
    [Route("api/json")]
    public class RestController : Controller
    {
        readonly string url = "https://api.coinmarketcap.com/v2/";

        // GET: api/<controller>
        [HttpGet]
        public async Task<Listings> GetAllListings(int limit = 0)
        {
            Listings response = await RequestBuilder.Create()
                .WithBaseUrl( url )
                .GetAsync<Listings>( "listings" );

            // Only return the top first 25 results
            return new Listings() {
                Data = limit == 0 ? response.Data : response.Data.Take( limit ),
                Metadata = response.Metadata
            };
        }
    }
}
