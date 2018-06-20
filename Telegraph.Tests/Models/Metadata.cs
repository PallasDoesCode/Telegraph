using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegraph.Tests.Models
{
    public class Metadata
    {
        public string Timestamp { get; set; }
        public long NumberOfCryptocurrencies { get; set; }
        public string Error { get; set; }
    }
}