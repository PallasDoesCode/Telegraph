using System;
using Xunit;
using spidey;

namespace spidey.tests
{
    public class SpideyTests
    {
        string url = "http://pokeapi.co/api/v2/pokemon/25";

        [Fact]
        public void AimAndFireCanGetResponse()
        {
            var response = WebShooter.Instance.AimAndFire( url );

            Assert.NotNull( response );
        }
    }
}
