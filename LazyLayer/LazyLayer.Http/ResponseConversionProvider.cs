using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using LazyLayer.Core.Providers;
using LazyLayer.Core.Responses;
using Newtonsoft.Json;

namespace LazyLayer.Http
{
    internal class ResponseConversionProvider : IResponseConversionProvider<IHttpActionResult>
    {
        private readonly ApiController _controller;

        /// <summary>
        /// INitializes new instance of <see cref="ResponseConversionProvider"/>
        /// </summary>
        /// <param name="controller">Instance of <see cref="ApiController"/>.</param>
        public ResponseConversionProvider(ApiController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Converts service result to <see cref="IHttpActionResult"/>.
        /// </summary>
        /// <param name="response">Instance of <see cref="IHttpActionResult"/>.</param>
        /// <returns></returns>
        public IHttpActionResult ConvertResponse(IServiceResponse response)
        {
            var content = response.GetType().GetProperty("Content")?.GetValue(response);
            HttpMethod method = _controller.Request.Method;

            if(response.Ex != null)
            {
                return CreateErrorResponse(
                        response.Message,
                        $"{method.Method}: {_controller.Request.RequestUri}",
                        response.CorrelationId);
            }

            if(method == HttpMethod.Post)
            {
                return
                        new CreatedAtRouteNegotiatedContentResult<int>(
                            $"Get{_controller.ControllerContext.ControllerDescriptor.ControllerName}ById",
                            new Dictionary<string, object> { { "id", content } },
                            (int)content,
                            _controller);
            }

            if(method == HttpMethod.Get)
            {
                if(content != null)
                    return new JsonResult<object>(content, new JsonSerializerSettings(), Encoding.UTF8, _controller);
                else
                    return new NotFoundResult(_controller);
            }

            return new OkResult(_controller);

            IHttpActionResult CreateErrorResponse(string message, string requestUrl, Guid correlationId)
            {
                var error = new { message, requestUrl, correlationId };
                var r = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = error.message,
                    Content = new ObjectContent(error.GetType(), error, new JsonMediaTypeFormatter())
                };

                return (IHttpActionResult)r;
            }
        }
    }
}