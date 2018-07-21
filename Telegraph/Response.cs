using System.Net;
using System.Net.Http.Headers;

namespace TelegraphSharp
{
    public class Response<T>
    {
        #region Properties

        public T Content { get; set; }
        public HttpResponseHeaders Headers { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        #endregion
    }
}
