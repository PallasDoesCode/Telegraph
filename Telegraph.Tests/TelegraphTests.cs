using Xunit;

namespace Telegraph.Tests
{
    public class TelegraphTests
    {
        string url = "http://pokeapi.co/api/v2/pokemon/25";

        [Fact]
        public void AimAndFireCanGetResponse()
        {
            var response = new RequestBuilder()
                .WithBaseUrl(url)
                .GetAsync();

            Assert.NotNull( response );
        }
    }
}
