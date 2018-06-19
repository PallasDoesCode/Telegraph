using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Telegraph
{
    public interface IRequest
    {
        #region Properties

        /// <summary>
        /// Gets the underlying HTTP request message.
        /// </summary>
        HttpRequestMessage Message { get; }

        /// <summary>
        /// Gets or sets the <see cref="HttpMethod"/> for the HTTP request.
        /// </summary>
        HttpMethod Method { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="System.Uri"/> for the HTTP request.
        /// </summary>
        Uri Uri { get; set; }

        /// <summary>
        /// Gets the collection of HTTP request headers.
        /// </summary>
        HttpRequestHeaders Headers { get;  }

        /// <summary>
        /// Determine whether has success status otherwise it will throw or not.
        /// </summary>
        bool HasSuccessStatusOrThrow { get; set; }

        #endregion
    }
}