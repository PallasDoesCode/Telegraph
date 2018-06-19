using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Telegraph
{
    /// <summary>Asynchronously parses an HTTP response.</summary>
    public interface IResponse
    {
        /// <summary>
        /// Gets the underlying HTTP response message.
        /// </summary>
        HttpResponseMessage Message { get; }

        /// <summary>
        /// Gets or sets the status code of the HTTP response.
        /// </summary>
        HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the content of HTTP response.
        /// </summary>
        HttpContent Content { get; set;  }

        /// <summary>
        /// Determine whether the HTTP response was successful.
        /// </summary>
        bool IsSuccessStatusCode { get;}

        /// <summary>
        /// Gets the collection of HTTP response headers.
        /// </summary>
        HttpResponseHeaders ResponseHeaders { get; }
    }
}