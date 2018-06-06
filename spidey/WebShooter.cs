using System;
using System.Net.Http;

namespace spidey
{
    public class WebShooter
    {
		// Static members are 'eagerly initialized', that is, 
		// immediately when class is loaded for the first time.
		// .NET guarantees thread safety for static initialization
		private readonly static Lazy<WebShooter> _instance = new Lazy<WebShooter>(() => new WebShooter());

        private HttpClient client = new HttpClient();

        private WebShooter()
        {
            Init();
        }

		public static WebShooter Instance
		{
			get
			{
				return _instance.Value;
			}
		}

        private void Init()
        {
            
        }

        public HttpResponseMessage AimAndFire(string url) {
            var uri = new Uri(url);

            return client.GetAsync(uri).Result;
        }

    }
}
