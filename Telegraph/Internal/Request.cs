using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Telegraph.Internal
{
    public class Request : IRequest
    {
        #region Properties

        /// <summary>
        /// Gets the underlying HTTP request message.
        /// </summary>
        public HttpRequestMessage Message { get; }

        /// <summary>
        /// Gets or sets the <see cref="HttpMethod"/> for the HTTP request.
        /// </summary>
        public HttpMethod Method
        {
            get => Message.Method;
            set => Message.Method = value;
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Uri"/> for the HTTP request.
        /// </summary>
        public Uri Uri
        {
            get => Message.RequestUri;
            set => Message.RequestUri = value;
        }

        /// <summary>
        /// Gets the collection of HTTP request headers.
        /// </summary>
        public HttpRequestHeaders Headers => Message.Headers;

        /// <summary>
        /// Determine whether has success status otherwise it will throw or not.
        /// </summary>
        public bool HasSuccessStatusOrThrow { get; set; }

        #endregion

        #region Constructor

        public Request(HttpRequestMessage message)
        {
            Message = message;
        }

        #endregion
    }
}
