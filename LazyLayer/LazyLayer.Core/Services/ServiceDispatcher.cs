using System;
using System.Threading.Tasks;
using LazyLayer.Core.Contracts;
using LazyLayer.Core.Providers;

namespace LazyLayer.Core.Services
{
    /// <summary>
    /// <see cref="IServiceDispatcher{TResponse}"/>
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public class ServiceDispatcher<TResponse> : IServiceDispatcher<TResponse>
    {
        public delegate TResponse ConvertResponse(BaseResponse converter);

        private readonly ConvertResponse _converter;
        private readonly IServiceExecutor _executor;

        #region Constructors

        /// <summary>
        /// Initializes new instance of <see cref="ServiceDispatcher{TResponse}"/>
        /// </summary>
        /// <param name="converter">
        /// Delegate that implements logic for conversion of <see cref="BaseResponse"/> to <see cref="TResponse"/>.
        /// </param>
        public ServiceDispatcher(ConvertResponse converter) : this(converter, null)
        {
        }


        public ServiceDispatcher(ConvertResponse converter, ILogProvider logger) : this(converter, logger, null)
        {
        }

        public ServiceDispatcher(ConvertResponse converter, ILogProvider logger, IValidationProvider validator)
        {
            _converter = converter;

            if (_converter == null)
            {
                Guard.ThrowIfNull(_converter, nameof(_converter));
            }

            Logger = logger ?? new NullLogProvider();
            _executor = new ServiceExecutor(logger, validator);
        }

        #endregion

        #region IServiceDispatcher

        public ILogProvider Logger { get; }

        public async Task<TResponse> ExecuteAsync(ServiceRequest request, Func<Task> action)
        {
            Guard.ThrowIfNull(request, nameof(request));
            Guard.ThrowIfNull(action, nameof(action));

            try
            {
                Logger.Verbose(request.CorrelationId, $"Received request: {request} at: {DateTime.UtcNow}");

                var response = await _executor.Invoke(request, action);
                var convertedResponse = _converter(response);

                return convertedResponse;
            }
            catch (Exception ex)
            {
                Logger.Fatal(
                    request.CorrelationId,
                    $"Unexpected error occurred: {Environment.NewLine}{ex.Message}." +
                    "{Environment.NewLine}Stack trace:{Environment.NewLine}{ex}");

                throw;
            }
            finally
            {
                Logger.Verbose(request.CorrelationId, "Request processed.");
            }
        }

        public async Task<TResponse> ExecuteAsync<TContent>(ServiceRequest<TContent> request, Func<TContent, Task> action)
        {
            Guard.ThrowIfNull(request, nameof(request));
            Guard.ThrowIfNull(action, nameof(action));

            try
            {
                Logger.Verbose(request.CorrelationId, $"Received request: {request} at: {DateTime.UtcNow}");

                var response = await _executor.Invoke(request, action);
                var convertedResponse = _converter(response);

                return convertedResponse;
            }
            catch (Exception ex)
            {
                Logger.Fatal(
                    request.CorrelationId,
                    $"Unexpected error occurred: {Environment.NewLine}{ex.Message}." +
                    "{Environment.NewLine}Stack trace:{Environment.NewLine}{ex}");

                throw;
            }
            finally
            {
                Logger.Verbose(request.CorrelationId, "Request processed.");
            }
        }

        public async Task<TResponse> ExecuteAsync<TResult>(ServiceRequest request, Func<Task<TResult>> action)
        {
            Guard.ThrowIfNull(request, nameof(request));
            Guard.ThrowIfNull(action, nameof(action));

            try
            {
                Logger.Verbose(request.CorrelationId, $"Received request: {request} at: {DateTime.UtcNow}");

                var response = await _executor.Invoke(request, action);
                var convertedResponse = _converter(response);

                return convertedResponse;
            }
            catch (Exception ex)
            {
                Logger.Fatal(
                    request.CorrelationId,
                    $"Unexpected error occurred: {Environment.NewLine}{ex.Message}." +
                    "{Environment.NewLine}Stack trace:{Environment.NewLine}{ex}");

                throw;
            }
            finally
            {
                Logger.Verbose(request.CorrelationId, "Request processed.");
            }
        }

        /// <summary>
        /// <see cref="IServiceDispatcher{TResponse}.ExecuteAsync{T, TResult}"/>
        /// </summary>
        public async Task<TResponse> ExecuteAsync<T, TResult>(ServiceRequest<T> request, Func<T, Task<TResult>> action)
        {
            Guard.ThrowIfNull(request, nameof(request));
            Guard.ThrowIfNull(action, nameof(action));

            try
            {
                Logger.Verbose(request.CorrelationId, $"Received request: {request} at: {DateTime.UtcNow}");

                var response = await _executor.Invoke(request, action);
                var convertedResponse = _converter(response);

                return convertedResponse;
            }
            catch (Exception ex)
            {
                Logger.Fatal(
                    request.CorrelationId,
                    $"Unexpected error occurred: {Environment.NewLine}{ex.Message}." +
                    "{Environment.NewLine}Stack trace:{Environment.NewLine}{ex}");

                throw;
            }
            finally
            {
                Logger.Verbose(request.CorrelationId, "Request processed.");
            }
        }

        #endregion
    }
}