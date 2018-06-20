using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Telegraph.Internal;

namespace Telegraph
{
    public class RequestBuilder
    {
        #region Properties

        string BaseUrl { get; set; }
        Dictionary<string, object> Parameters { get; set; }
        HttpClient client = new HttpClient();

        HttpRequestMessage RequestMessage { get; set; }
        HttpResponseMessage ResponseMessage { get; set; }
        HttpMethod Method { get; set; }
        Uri Uri { get; set; }
        HttpRequestHeaders Headers { get; set; }
        bool HasSuccessStatusOrThrow { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set base url for each request.
        /// </summary>
        /// <param name="url">Base url address to set. e.g. "https://api.mysite.com"</param>
        /// <returns>Returns client builder for chaining.</returns>
        public RequestBuilder WithBaseUrl(string url)
        {
            BaseUrl = url;
            return this;
        }

        public RequestBuilder WithQueryParameters(Dictionary<string, object> parameters)
        {
            Parameters = parameters;
            return this;
        }

        /// <summary>Add an authentication header.</summary>
        /// <param name="scheme">The scheme to use for authorization. e.g.: "Basic", "Bearer".</param>
        /// <param name="parameter">The credentials containing the authentication information.</param>
        public RequestBuilder WithAuthentication(string scheme, string parameter)
        {
            RequestMessage.Headers.Authorization = new AuthenticationHeaderValue(scheme, parameter);
            return this;
        }

        public async Task<RequestBuilder> GetAsync(string queryParameters = "")
        {
            ResponseMessage = await client.GetAsync(queryParameters);
            return this;
        }

        public async Task<RequestBuilder> PostAsync<T>(string queryParameters = "", T data = null)
            where T : class
        {
            var content = new ObjectContent<T>(data, new JsonMediaTypeFormatter());
            ResponseMessage = await client.PostAsync(queryParameters, content);
            return this;
        }

        public T ReturnAs<T>() where T : class, new()
        {
            T result = null;
            if (ResponseMessage.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                result = ResponseMessage.Content.ReadAsAsync<T>().Result;
            }

            return result;
        }

        // CONVERSION OPERATOR
        public static implicit operator Request(RequestBuilder rb)
        {
            return new Request( rb.RequestMessage );
        }

        #endregion
    }
}
