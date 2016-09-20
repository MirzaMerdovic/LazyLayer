using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Results;

namespace LazyLayer.Http
{
    public class ProcessingFailedResult : ResponseMessageResult
    {
        public ProcessingFailedResult(HttpResponseMessage response) : base(response)
        {
        }

        public ProcessingFailedResult(ErrorInfo error) : base(Create(error))
        {
        }

        private static HttpResponseMessage Create(ErrorInfo error)
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                ReasonPhrase = error.Message,
                Content = new ObjectContent(typeof(ErrorInfo), error, new JsonMediaTypeFormatter())
            };
        }
    }
}