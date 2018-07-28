using LazyLayer.Core.Providers;
using LazyLayer.Core.Requests;
using LazyLayer.Core.Responses;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace LazyLayer.Core.Services.Dispatchers
{
    /// <summary>
    /// <see cref="IServiceDispatcher{TResponse}"/>
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public class ServiceDispatcher<TResponse> : IServiceDispatcher<TResponse>
    {
        #region Fields

        private readonly Func<ServiceResponse, TResponse> _converter;
        private readonly ILogProvider _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes new instance of <see cref="ServiceDispatcher{TResponse}"/>
        /// </summary>
        /// <param name="converter">
        /// Delegate that implements logic for conversion of <see cref="ServiceResponse"/> to <see cref="TResponse"/>.
        /// </param>
        public ServiceDispatcher(Func<ServiceResponse, TResponse> converter) : this(converter, null)
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="ServiceDispatcher{TResponse}"/>
        /// </summary>
        /// <param name="converter">
        /// Delegate that implements logic for conversion of <see cref="ServiceResponse"/> to <see cref="TResponse"/>.</param>
        /// <param name="logger">Implementation of <see cref="ILogProvider"/> interface.
        /// <remarks>If value is null <see cref="NullLogProvider"/> will be used, which does nothing.</remarks>
        /// </param>
        public ServiceDispatcher(Func<ServiceResponse, TResponse> converter, ILogProvider logger)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            _logger = logger ?? new NullLogProvider();
        }

        #endregion

        #region IServiceDispatcher

        public async virtual Task<TResponse> Execute(ServiceRequest request, IExecutor executor)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            executor = executor ?? throw new ArgumentNullException(nameof(executor));

            ServiceResponse response = null;

            try
            {
                MethodInfo run = executor.GetType().GetMethod("Run");
                var invoke = run.Invoke(executor, null);

                if (executor.IsVoid)
                {
                    await GetTaskResult(invoke);
                    response = new ServiceResponse(request.CorrelationId);
                }
                else
                {
                    var result = await GetTaskResult(invoke);
                    response = new ServiceResponse(request.CorrelationId, executor.IsVoid != false, result);
                }
            }
            catch (Exception ex)
            {
                response = new ServiceResponse(request.CorrelationId, executor.IsVoid, response)
                {
                    Message = $"Method: {executor.MethodName} execution failed",
                    Ex = ex
                };
            }

            var convertedResponse = _converter(response);

            return convertedResponse;

            async Task<dynamic> GetTaskResult(dynamic resultTask)
            {
                var r = await resultTask;

                return r;
            }
        }

        #endregion
    }
}