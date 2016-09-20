using System;
using System.Diagnostics;
using System.Threading.Tasks;
using LazyLayer.Core.Contracts;
using LazyLayer.Core.Providers;

namespace LazyLayer.Core.Services
{
    internal class ServiceExecutor : IServiceExecutor
    {
        #region Fields

        private readonly ILogProvider _logger;
        private readonly IValidationProvider _validator;

        #endregion

        #region Constructors

        public ServiceExecutor() : this(null, null)
        {
        }

        public ServiceExecutor(IValidationProvider validator) : this(null, validator)
        {
        }

        public ServiceExecutor(ILogProvider logger) : this(logger, null)
        {
        }

        public ServiceExecutor(ILogProvider logger, IValidationProvider validator)
        {
            _logger = logger ?? new NullLogProvider();
            _validator = validator ?? new NullValidationProvider();
        }

        #endregion

        #region IServiceExecutor

        public async Task<BaseResponse> Invoke(ServiceRequest request, Func<Task> method)
        {
            _logger.Information(request.CorrelationId, $"{method.Method.Name} => Started");

            var timer = new Stopwatch();

            try
            {
                timer.Start();
                await method();
                timer.Stop();

                _logger.Verbose(request.CorrelationId, $"{method.Method.Name} => Execution time: {timer.ElapsedMilliseconds}");

                return new OkResponse(request.CorrelationId);
            }
            catch (Exception ex)
            {
                _logger.Error(request.CorrelationId, ex, $"{method.Method.Name} => Failed.");

                return new FailedResponse(request.CorrelationId, ex, method.Method.Name);
            }
            finally
            {
                _logger.Information(request.CorrelationId, $"{method.Method.Name} => Finished");
            }
        }

        public async Task<BaseResponse> Invoke<TResult>(ServiceRequest request, Func<Task<TResult>> method)
        {
            _logger.Information(request.CorrelationId, $"{method.Method.Name} => Started");

            var timer = new Stopwatch();

            try
            {
                timer.Start();
                var result = await method();
                timer.Stop();

                _logger.Verbose(request.CorrelationId, $"{method.Method.Name} => Execution time: {timer.ElapsedMilliseconds}");

                return new OkResponse<TResult>(request.CorrelationId, result);
            }
            catch (Exception ex)
            {
                _logger.Error(request.CorrelationId, ex, $"{method.Method.Name} => Failed.");

                return new FailedResponse(request.CorrelationId, ex, method.Method.Name);
            }
            finally
            {
                _logger.Information(request.CorrelationId, $"{method.Method.Name} => Finished");
            }
        }

        public async Task<BaseResponse> Invoke<TContent>(ServiceRequest<TContent> request, Func<TContent, Task> method)
        {
            _logger.Information(request.CorrelationId, $"{method.Method.Name} => Started");

            var timer = new Stopwatch();

            try
            {
                if (!_validator.Validate(request.Content))
                {
                    return new ValidationFailedResponse(request.CorrelationId, _validator.GetErrors());
                }

                timer.Start();
                await method(request.Content);
                timer.Stop();

                _logger.Verbose(request.CorrelationId, $"{method.Method.Name} => Execution time: {timer.ElapsedMilliseconds}");

                return new OkResponse(request.CorrelationId);
            }
            catch (Exception ex)
            {
                _logger.Error(request.CorrelationId, ex, $"{method.Method.Name} => Failed.");

                return new FailedResponse(request.CorrelationId, ex, method.Method.Name);
            }
            finally
            {
                _logger.Information(request.CorrelationId, $"{method.Method.Name} => Finished");
            }
        }

        public async Task<BaseResponse> Invoke<TContent, TResult>(ServiceRequest<TContent> request, Func<TContent, Task<TResult>> method)
        {
            _logger.Information(request.CorrelationId, $"{method.Method.Name} => Started");

            try
            {
                if (!_validator.Validate(request.Content))
                {
                    return new ValidationFailedResponse(request.CorrelationId, _validator.GetErrors());
                }

                var result = await method(request.Content);

                return new OkResponse<TResult>(request.CorrelationId, result);
            }
            catch (Exception ex)
            {
                _logger.Error(request.CorrelationId, ex, $"{method.Method.Name} => Failed.");

                return new FailedResponse(request.CorrelationId, ex, method.Method.Name);
            }
            finally
            {
                _logger.Information(request.CorrelationId, $"{method.Method.Name} => Finished");
            }
        }

        #endregion
    }
}