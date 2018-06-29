using System.Net;

namespace Telegraph
{

    public class Response<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Content { get; set; }
    }
}
