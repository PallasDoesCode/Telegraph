using System.Collections.Generic;

namespace Telegraph.Tests.Models
{
    public class Listings
    {
        public IEnumerable<Listing> Data { get; set; }
        public Metadata Metadata { get; set; }
    }
}
