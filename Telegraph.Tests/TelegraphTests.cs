using Xunit;
using System.Collections.Generic;

namespace Telegraph.Tests
{
    public class TelegraphTests
    {
        string url = "http://pokeapi.co/api/v2/pokemon/25";

        [Fact]
        void Check_ResponseNotNullForGetAync()
        {
            var response = new RequestBuilder()
                .WithBaseUrl( url )
                .GetAsync( "" ).Result
                .ReturnAs<Pokemon>();

            Assert.NotNull( response );
        }

        [Fact]
        void Check_TypeCastResponse()
        {
            var response = new RequestBuilder()
                .WithBaseUrl( url )
                .GetAsync().Result
                .ReturnAs<Pokemon>();
            
            Assert.Matches( "Pikachu".ToLower(), response.Name.ToLower() );
        }

        void Check_ResponseNotNullForPostAync()
        {
            var response = new RequestBuilder()
                .WithBaseUrl( url )
                .PostAsync( queryParameters: "", data: new T() ).Result
                .ReturnAs<Pokemon>();
            
            Assert.NotNull( response );
        }
    }
}
