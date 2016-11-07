using System;
using System.Diagnostics;
using System.Threading.Tasks;
using LazyLayer.Core.Providers;
using LazyLayer.Core.Requests;
using LazyLayer.Core.Responses;

namespace LazyLayer.Core.Services
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
            _converter = converter;

            if (_converter == null)
            {
                Guard.ThrowIfNull(_converter, nameof(_converter));
            }

            _logger = logger ?? new NullLogProvider();
        }

        #endregion

        #region IServiceDispatcher

        public async Task<TResponse> ExecuteAsync(ServiceRequest request, Func<Task> action)
        {
            Guard.ThrowIfNull(request, nameof(request));
            Guard.ThrowIfNull(action, nameof(action));

            ServiceResponse response;
            _logger.Information(request.CorrelationId, $"{action.Method.Name} => Started");

            var timer = new Stopwatch();

            try
            {
                timer.Start();
                await action();
                timer.Stop();

                _logger.Verbose(request.CorrelationId, $"{action.Method.Name} => Execution time: {timer.ElapsedMilliseconds}.");

                response = new OkResponse(request.CorrelationId);
            }
            catch (Exception ex)
            {
                _logger.Error(request.CorrelationId, ex, $"{action.Method.Name} => Failed.");

                response = new FailedResponse(request.CorrelationId, ex, action.Method.Name);
            }
            finally
            {
                _logger.Information(request.CorrelationId, $"{action.Method.Name} => Finished.");
            }

            var convertedResponse = _converter(response);

            return convertedResponse;
        }

        public async Task<TResponse> ExecuteAsync<TContent>(ServiceRequest<TContent> request, Func<TContent, Task> action)
        {
            Guard.ThrowIfNull(request, nameof(request));
            Guard.ThrowIfNull(action, nameof(action));

            _logger.Information(request.CorrelationId, $"{action.Method.Name} => Started");

            ServiceResponse response;
            var timer = new Stopwatch();

            try
            {
                timer.Start();
                await action(request.Content);
                timer.Stop();

                _logger.Verbose(request.CorrelationId, $"{action.Method.Name} => Execution time: {timer.ElapsedMilliseconds}");

                response = new OkResponse(request.CorrelationId);
            }
            catch (Exception ex)
            {
                _logger.Error(request.CorrelationId, ex, $"{action.Method.Name} => Failed.");

                response = new FailedResponse(request.CorrelationId, ex, action.Method.Name);
            }
            finally
            {
                _logger.Information(request.CorrelationId, $"{action.Method.Name} => Finished");
            }

            var convertedResponse = _converter(response);

            return convertedResponse;
        }

        public async Task<TResponse> ExecuteAsync<TResult>(ServiceRequest request, Func<Task<TResult>> action)
        {
            Guard.ThrowIfNull(request, nameof(request));
            Guard.ThrowIfNull(action, nameof(action));

            _logger.Information(request.CorrelationId, $"{action.Method.Name} => Started");

            ServiceResponse response;
            var timer = new Stopwatch();

            try
            {
                timer.Start();
                var result = await action();
                timer.Stop();

                _logger.Verbose(request.CorrelationId, $"{action.Method.Name} => Execution time: {timer.ElapsedMilliseconds}");

                response = new OkResponse<TResult>(request.CorrelationId, result);
            }
            catch (Exception ex)
            {
                _logger.Error(request.CorrelationId, ex, $"{action.Method.Name} => Failed.");

                response = new FailedResponse(request.CorrelationId, ex, action.Method.Name);
            }
            finally
            {
                _logger.Information(request.CorrelationId, $"{action.Method.Name} => Finished");
            }

            var convertedResponse = _converter(response);

            return convertedResponse;
        }

        public async Task<TResponse> ExecuteAsync<T, TResult>(ServiceRequest<T> request, Func<T, Task<TResult>> action)
        {
            Guard.ThrowIfNull(request, nameof(request));
            Guard.ThrowIfNull(action, nameof(action));

            _logger.Information(request.CorrelationId, $"{action.Method.Name} => Started");
            ServiceResponse response;
            var timer = new Stopwatch();

            try
            {
                timer.Start();
                var result = await action(request.Content);
                timer.Stop();

                _logger.Verbose(request.CorrelationId, $"{action.Method.Name} => Execution time: {timer.ElapsedMilliseconds}");

                response = new OkResponse<TResult>(request.CorrelationId, result);
            }
            catch (Exception ex)
            {
                _logger.Error(request.CorrelationId, ex, $"{action.Method.Name} => Failed.");

                response = new FailedResponse(request.CorrelationId, ex, action.Method.Name);
            }
            finally
            {
                _logger.Information(request.CorrelationId, $"{action.Method.Name} => Finished");
            }

            var convertedResponse = _converter(response);

            return convertedResponse;
        }

        #endregion
    }
}