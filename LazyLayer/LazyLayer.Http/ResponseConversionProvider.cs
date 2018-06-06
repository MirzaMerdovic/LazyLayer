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
        /// <param name="result">Instance of <see cref="IHttpActionResult"/>.</param>
        /// <returns></returns>
        public IHttpActionResult ConvertResponse(IServiceResponse result)
        {
            switch (result.Status)
            {
                case ResponseStatus.Success:
                    return new OkResult(_controller);

                case ResponseStatus.Created:
                    var id = result.GetType().GetProperty("Content").GetValue(result);

                    return 
                        new CreatedAtRouteNegotiatedContentResult<int>(
                            $"Get{_controller.ControllerContext.ControllerDescriptor.ControllerName}ById", 
                            new Dictionary<string, object> { { "id", id } }, 
                            (int)id, 
                            _controller);

                case ResponseStatus.Found:
                    var content = result.GetType().GetProperty("Content").GetValue(result);

                    return new JsonResult<object>(content, new JsonSerializerSettings(), Encoding.UTF8, _controller);

                case ResponseStatus.NotFound:
                    return new NotFoundResult(_controller);

                case ResponseStatus.Failure:
                    var response = (FailedResponse)result;

                    return CreateErrorResponse(
                        response.Message, 
                        $"{_controller.Request.Method}: {_controller.Request.RequestUri}", 
                        response.CorrelationId);

                case ResponseStatus.Unknown:
                    return new ExceptionResult(((FailedResponse)result).Exception, _controller);

                default:
                    return new InternalServerErrorResult(_controller);
            }

            IHttpActionResult CreateErrorResponse(string message, string requestUrl, Guid correlationId)
            {
                var error = new { message, requestUrl, correlationId };
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = error.message,
                    Content = new ObjectContent(error.GetType(), error, new JsonMediaTypeFormatter())
                };

                return (IHttpActionResult)response;
            }
        }
    }
}