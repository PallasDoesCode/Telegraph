using CoinMarketCap.Core.Models;
using System.Linq;
using Xunit;

namespace Telegraph.Tests
{
    public class TelegraphTests
    {
        readonly string url = "https://api.coinmarketcap.com/v2/";

        [Fact]
        async void Check_ResponseNotNullForGetAync()
        {
            var response = await RequestBuilder.Create()
                .WithBaseUrl(url)
                .GetAsync<Listings>("listings");

            Assert.NotNull( response );
        }

        [Fact]
        async void Check_TypeCastResponseForGetAsync()
        {
            var response = await RequestBuilder.Create()
                .WithBaseUrl(url)
                .GetAsync<Listings>("listings");

            Listing currency = response.Content.Data.ElementAt(0);
            
            Assert.Matches( "Bitcoin".ToLower(), currency.Name.ToLower() );
        }

        [Fact(Skip="API does not support post requests")]
        void Check_ResponseNotNullForPostAync()
        {
        }

        [Fact(Skip = "API does not support post requests")]
        void Check_ResponseNotNullWithPostPayload()
        {
            
        }

        [Fact(Skip = "API does not support post requests")]
        void Check_CanUseAuthorization() {

        }
    }
}
