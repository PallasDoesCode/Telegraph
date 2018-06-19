using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Telegraph.Internal
{
    public class Response : IResponse
    {
        #region Properties

        /// <summary>
        /// Gets the underlying HTTP response message.
        /// </summary>
        public HttpResponseMessage Message { get; }

        /// <summary>
        /// Gets or sets the status code of the HTTP response.
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get => Message.StatusCode;
            set => Message.StatusCode = value;
        }

        /// <summary>
        /// Gets or sets the content of HTTP response.
        /// </summary>
        public HttpContent Content
        {
            get => Message.Content;
            set => Message.Content = value;
        }

        /// <summary>
        /// Determine whether the HTTP response was successful.
        /// </summary>
        public bool IsSuccessStatusCode => Message.IsSuccessStatusCode;

        /// <summary>
        /// Gets the collection of HTTP response headers.
        /// </summary>
        public HttpResponseHeaders ResponseHeaders => Message.Headers;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new <see cref="FluentHttpResponse"/>.
        /// </summary>
        /// <param name="message"></param>
        public Response(HttpResponseMessage message)
        {
            Message = message;
        }

        #endregion
    }
}