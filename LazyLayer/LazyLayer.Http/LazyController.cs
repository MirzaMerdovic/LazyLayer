using LazyLayer.Core.Providers;
using LazyLayer.Core.Requests;
using LazyLayer.Core.Services;
using LazyLayer.Core.Services.Dispatchers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Http;

namespace LazyLayer.Http
{
    public class LazyController : ApiController
    {
        private readonly IServiceDispatcher<IHttpActionResult> _dispatcher;
        private readonly string _userName;

        #region Constructors

        /// <summary>
        /// Initializes new instance of <see cref="LazyController"/>.
        /// </summary>
        protected LazyController() : this(null, null)
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="LazyController"/>.
        /// </summary>
        /// <param name="convertor">Instance of <see cref="IResponseConversionProvider{TConvertedResponse}"/>.</param>
        protected LazyController(IResponseConversionProvider<IHttpActionResult> convertor) : this(convertor, null)
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="LazyController"/>.
        /// </summary>
        /// <param name="convertor">Instance of <see cref="IResponseConversionProvider{TConvertedResponse}"/>.</param>
        /// <param name="logger"></param>
        protected LazyController(IResponseConversionProvider<IHttpActionResult> convertor, ILogProvider logger)
        {
            _dispatcher =
                ServiceDispatcherFactory<IHttpActionResult>
                    .Create(
                        convertor ?? new ResponseConversionProvider(this),
                        logger ?? new NullLogProvider());

            _userName = User?.Identity?.Name ?? "Unknown";
        }

        private bool AddSqlException(SqlException sex)
        {
            return false;
        }

        #endregion

        #region Execute

        protected Task<IHttpActionResult> ExecuteAsync(Func<Task> method)
        {
            return
                _dispatcher.Execute(
                    new ServiceRequest
                    {
                        UserName = _userName
                    },
                    new FuncTaskExecutor(method));
        }

        protected Task<IHttpActionResult> ExecuteAsync<TContent>(TContent content, Func<TContent, Task> method)
        {
            return
                _dispatcher.Execute(
                    new ServiceRequest<TContent>(content)
                    {
                        UserName = _userName
                    },
                    new FuncOfTTaskExecutor<TContent>(content, method));
        }


        protected Task<IHttpActionResult> ExecuteAsync<TResult>(Func<Task<TResult>> method)
        {
            return
                _dispatcher.Execute(
                    new ServiceRequest
                    {
                        UserName = _userName
                    },
                    new FuncTaskOfTExecutor<TResult>(method));
        }

        protected Task<IHttpActionResult> ExecuteAsync<TContent, TResult>(TContent content, Func<TContent, Task<TResult>> method)
        {
            return
                _dispatcher.Execute(
                    new ServiceRequest<TContent>(content)
                    {
                        UserName = _userName
                    },
                    new FuncOfTTaskOfTExecutor<TContent, TResult>(content, method));
        }

        #endregion
    }
}