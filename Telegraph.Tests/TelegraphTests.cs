using System.Linq;
using Telegraph.Tests.Models;
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
            
            Assert.Matches( "Bitcoin".ToLower(), response.Data.ElementAt(0).Name.ToLower() );
        }

        [Fact]
        async void Check_ResponseNotNullForPostAync()
        {
            //var response = await RequestBuilder.Create()
            //    .WithBaseUrl( url )
            //    .PostAsync<Listings>( content: new Listing(), queryParameters: "" );
            
            //Assert.NotNull( response );
        }

        [Fact]
        async void Check_ResponseNotNullWithPostPayload() {
            //var response = new RequestBuilder()
            //    .WithBaseUrl(url)
            //    .PostAsync(queryParameters: "", data: new T()).Result
            //    .ReturnAs<Listing>();

            //Assert.NotNull(response);
        }

        [Fact]
        async void Check_CanUseAuthorization() {

        }
    }
}
