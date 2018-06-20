using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Telegraph
{
    public class RequestBuilder : IDisposable
    {
        #region Fields

        readonly HttpClient client;

        #endregion

        #region Properties

        HttpRequestMessage RequestMessage { get; set; }

        #endregion

        #region Constructor

        private RequestBuilder()
        {
            client = new HttpClient();
        }

        public void Dispose()
        {
            client.Dispose();
        }

        #endregion

        #region Public Methods

        public static RequestBuilder Create()
        {
            return new RequestBuilder();
        }

        /// <summary>
        /// Set base url for each request.
        /// </summary>
        /// <param name="url">Base url address to set. e.g. "https://api.mysite.com"</param>
        /// <returns>Returns client builder for chaining.</returns>
        public RequestBuilder WithBaseUrl(string url, Dictionary<string, object> queryParameters = null)
        {
            var uriBuilder = new UriBuilder(url)
            {
                Query = QueryString(queryParameters)
            };
            client.BaseAddress = uriBuilder.Uri;
            
            return this;
        }

        /// <summary>Add an authentication header.</summary>
        /// <param name="scheme">The scheme to use for authorization. e.g.: "Basic", "Bearer".</param>
        /// <param name="parameter">The credentials containing the authentication information.</param>
        public RequestBuilder WithAuthorization(string scheme, string parameter)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, parameter);
            return this;
        }

        public async Task<T> GetAsync<T>(string queryParameters = "")
        {
            var response = await client.GetAsync(queryParameters);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<T> PostAsync<T>(HttpContent content = null, string queryParameters = "")
        {
            var response = await client.PostAsync(queryParameters, content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<T> PostJsonAsync<T, U>(U payload, string queryParameters = "")
        {
            var content = new ObjectContent<U>(payload, new JsonMediaTypeFormatter());

            return await PostAsync<T>(content, queryParameters);
        }

        #endregion

        #region Private Methods

        private string QueryString(IDictionary<string, object> dict)
        {
            if (dict == null)
            {
                return null;
            }

            var list = new List<string>();
            foreach ( var item in dict )
            {
                list.Add(item.Key + "=" + item.Value);
            }
            return string.Join("&", list);
        }

        #endregion
    }
}
