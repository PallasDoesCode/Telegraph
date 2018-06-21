using System.Threading.Tasks;
using CoinMarketCap.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Telegraph.UI.Controllers {
    [Route("api/json")]
    public class RestController : Controller
    {
        readonly string url = "https://api.coinmarketcap.com/v2/";

        // GET: api/<controller>
        [HttpGet]
        public async Task<Tickers> GetAllListings()
        {
            var response = await RequestBuilder.Create()
                .WithBaseUrl( url )
                .GetAsync<Tickers>( "ticker?limit=25" );

            return response;
        }
    }
}
