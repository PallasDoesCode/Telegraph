using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Telegraph.Internal;

namespace Telegraph
{
    public class RequestBuilder
    {
        private string _baseUrl;
        private Dictionary<string, object> _dictParameters;

        #region Properties

        public HttpRequestMessage RequestMessage { get; set; }
        public HttpResponseMessage ResponseMessage { get; set; }

        #endregion

        #region Public Methods

        /// <summary>Free resources used by the client.</summary>
        public virtual void Dispose()
        {
            //this.Dispose(true);
            //GC.SuppressFinalize(this); // garbage collector method?!?! not sure what this does
        }

        /// <summary>
        /// Set base url for each request.
        /// </summary>
        /// <param name="url">Base url address to set. e.g. "https://api.mysite.com"</param>
        /// <returns>Returns client builder for chaining.</returns>
        public RequestBuilder WithBaseUrl(string url)
        {
            _baseUrl = url;
            return this;
        }

        public RequestBuilder WithQueryParameters<T>(Dictionary<string, object> parameters) where T : class
        {
            _dictParameters = parameters;
            return this;
        }

        /// <summary>Add an authentication header.</summary>
        /// <param name="scheme">The scheme to use for authorization. e.g.: "Basic", "Bearer".</param>
        /// <param name="parameter">The credentials containing the authentication information.</param>
        public RequestBuilder WithAuthentication(string scheme, string parameter)
        {
            this.RequestMessage.Headers.Authorization = new AuthenticationHeaderValue(scheme, parameter);
            return this;
        }

        public RequestBuilder GetAsync()
        {
            return this;
        }

        public RequestBuilder PostAsync()
        {
            return this;
        }


        // CONVERSION OPERATOR
        public static implicit operator Response(RequestBuilder rb)
        {
            return new Response(
                rb.ResponseMessage
            );
        }

        #endregion
    }
}
