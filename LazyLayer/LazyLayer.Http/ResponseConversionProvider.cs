using System;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using LazyLayer.Core.Contracts;
using LazyLayer.Core.Providers;
using Newtonsoft.Json;

namespace LazyLayer.Http
{
    internal class ResponseConversionProvider : IResponseConversionProvider<IHttpActionResult>
    {
        private readonly ApiController _controller;

        public ResponseConversionProvider(ApiController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Converts service result to <see cref="IHttpActionResult"/>.
        /// </summary>
        /// <param name="result">Instance of <see cref="IHttpActionResult"/>.</param>
        /// <returns></returns>
        public IHttpActionResult ConvertResponse(IResponseStatus result)
        {
            switch (result.Status)
            {
                case ResponseStatus.Success:
                    return new OkResult(_controller);

                case ResponseStatus.Found:
                    var content = result.GetType().GetProperty("Content").GetValue(result);

                    return new JsonResult<object>(content, new JsonSerializerSettings(), Encoding.UTF8, _controller);

                case ResponseStatus.NotFound:
                    return new NotFoundResult(_controller);

                case ResponseStatus.Failure:
                    var error =
                        ErrorInfo.Create(
                            ((FailedResponse) result).Message,
                            $"{_controller.Request.Method}: {_controller.Request.RequestUri}",
                            ((FailedResponse) result).CorrelationId);

                    return new ProcessingFailedResult(error);

                case ResponseStatus.Unknown:
                    return new ExceptionResult(((FailedResponse)result).Exception, _controller);

                default:
                    return new InternalServerErrorResult(_controller);
            }
        }
    }
}