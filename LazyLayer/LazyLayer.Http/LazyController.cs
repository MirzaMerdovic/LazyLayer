using System;
using System.Threading.Tasks;
using System.Web.Http;
using LazyLayer.Core.Providers;
using LazyLayer.Core.Requests;
using LazyLayer.Core.Services;

namespace LazyLayer.Http
{
    public class LazyController : ApiController
    {
        protected readonly IServiceDispatcher<IHttpActionResult> Dispatcher;

        #region Constructors

        /// <summary>
        /// Initializes new instance of <see cref="LazyController"/>.
        /// </summary>
        protected LazyController()
        {
            Dispatcher = ServiceDispatcherFactory<IHttpActionResult>.Create(new ResponseConversionProvider(this));
        }

        /// <summary>
        /// Initializes new instance of <see cref="LazyController"/>.
        /// </summary>
        /// <param name="convertor">Instance of <see cref="IResponseConversionProvider{TConvertedResponse}"/>.</param>
        protected LazyController(IResponseConversionProvider<IHttpActionResult> convertor)
        {
            Dispatcher = ServiceDispatcherFactory<IHttpActionResult>.Create(convertor);
        }

        #endregion

        #region Execute

        protected Task<IHttpActionResult> ExecuteAsync(Func<Task> method)
        {
            return Dispatcher.ExecuteAsync(new ServiceRequest(), method);
        }

        protected Task<IHttpActionResult> ExecuteAsync<TContent>(TContent content, Func<TContent, Task> method)
        {
            return Dispatcher.ExecuteAsync(new ServiceRequest<TContent>(content), method);
        }


        protected Task<IHttpActionResult> ExecuteAsync<TResult>(Func<Task<TResult>> method)
        {
            return Dispatcher.ExecuteAsync(new ServiceRequest(), method);
        }

        protected Task<IHttpActionResult> ExecuteAsync<TContent, TResult>(TContent content, Func<TContent, Task<TResult>> method)
        {
            return Dispatcher.ExecuteAsync(new ServiceRequest<TContent>(content), method);
        }

        #endregion
    }
}