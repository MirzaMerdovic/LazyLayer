using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Results;
using LazyLayer.Core.Contracts;

namespace LazyLayer.Http
{
    public class ValidationFailedResult : ResponseMessageResult
    {
        public ValidationFailedResult(HttpResponseMessage response) : base(response)
        {
        }

        public ValidationFailedResult(IEnumerable<ValidationError> errors) : base(Create(errors))
        {
        }

        private static HttpResponseMessage Create(IEnumerable<ValidationError> errors)
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                ReasonPhrase = "Invalid request.",
                Content = new ObjectContent(typeof(IEnumerable<ValidationError>), errors, new JsonMediaTypeFormatter())
            };
        }
    }
}