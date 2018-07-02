using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace TelegraphSharp
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
        /// <param name="queryParameters">Query paramerters to add to the base url.</param>
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

        public RequestBuilder WithRequestHeaders<THeader>(Dictionary<string, THeader> headers) {
            foreach (var header in headers) {
                var json = JsonConvert.SerializeObject( header.Value );
                client.DefaultRequestHeaders.Add( header.Key, json );
            }

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

        /// <summary>Performs an asyncronous GET request against a given URL</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryParameters"></param>
        /// <returns>Returns a Response object</returns>
        public async Task<Response<T>> GetAsync<T>(string queryParameters = "")
        {
            var response = await client.GetAsync(queryParameters);

            return new Response<T> {
                Content = await response.Content.ReadAsAsync<T>(),
                Headers = response.Headers,
                StatusCode = response.StatusCode
            };
        }

        /// <summary>Performs a synchronous GET request against a given URL</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryParameters"></param>
        /// <returns>Returns a Response object</returns>
        public async Task<Response<T>> Get<T>(string queryParameters = "") {
            var response = client.GetAsync(queryParameters).Result;

            return new Response<T> {
                Content = await response.Content.ReadAsAsync<T>(),
                Headers = response.Headers,
                StatusCode = response.StatusCode
            };
        }

        /// <summary>Performs an asynchronous POST request against a given URL</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="queryParameters"></param>
        /// <returns>Returns a Response object</returns>
        public async Task<Response<T>> PostAsync<T>(HttpContent content = null, string queryParameters = "") {
            var response = await client.PostAsync(queryParameters, content);

            return new Response<T> {
                Content = await response.Content.ReadAsAsync<T>(),
                Headers = response.Headers,
                StatusCode = response.StatusCode
            };
        }

        /// <summary>Performs a synchronous POST request against a given URL</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="queryParameters"></param>
        /// <returns>Returns a Response object</returns>
        public async Task<Response<T>> Post<T>(HttpContent content = null, string queryParameters = "") {
            var response = client.PostAsync(queryParameters, content).Result;

            return new Response<T> {
                Content = await response.Content.ReadAsAsync<T>(),
                Headers = response.Headers,
                StatusCode = response.StatusCode
            };
        }

        /// <summary>Performs an asynchronous POST request against a given URL</summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="payload"></param>
        /// <param name="queryParameters"></param>
        /// <returns>Returns a Response object</returns>
        public async Task<Response<TDestination>> PostJsonAsync<TSource, TDestination>(TSource payload, string queryParameters = "")
        {
            var content = new ObjectContent<TSource>(payload, new JsonMediaTypeFormatter());

            return await PostAsync<TDestination>(content, queryParameters);
        }

        /// <summary>Performs a synchronous POST request against a given URL</summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="payload"></param>
        /// <param name="queryParameters"></param>
        /// <returns>Returns a Response object</returns>
        public Task<Response<TDestination>> PostJson<TSource, TDestination>(TSource payload, string queryParameters = "") {
            var content = new ObjectContent<TSource>(payload, new JsonMediaTypeFormatter());

            return Post<TDestination>(content, queryParameters);
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
