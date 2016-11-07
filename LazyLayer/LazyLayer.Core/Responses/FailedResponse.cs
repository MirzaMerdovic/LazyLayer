using System;

namespace LazyLayer.Core.Responses
{
    /// <summary>
    /// Represents service execution failure.
    /// </summary>
    public class FailedResponse : ServiceResponse
    {
        /// <summary>
        /// <see cref="Exception"/> that has been thrown.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Initializes new instance of <see cref="FailedResponse"/>.
        /// </summary>
        /// <param name="correlationId"><see cref="ServiceResponse.CorrelationId"/>.</param>
        /// <param name="exception"><see cref="Exception"/>.</param>
        /// <param name="methodName">Method that caused the failure.</param>
        public FailedResponse(Guid correlationId, Exception exception, string methodName) : base(correlationId)
        {
            Status = ResponseStatus.Failure;
            Exception = exception;
            Message = $"{methodName} failed with error: {exception.Message}";
        }

        /// <summary>
        /// Initializes new instance of <see cref="FailedResponse"/>.
        /// </summary>
        /// <param name="correlationId"><see cref="Guid"/> value that will be used as a correlation identifier for tracking purposes.</param>
        /// <param name="errorMessage">Message that carries information about the failure or thrown exception.</param>
        /// <param name="methodName">Method that caused the failure.</param>
        public FailedResponse(Guid correlationId, string errorMessage, string methodName) : base(correlationId)
        {
            Status = ResponseStatus.Failure;
            Message = $"{methodName} failed with error: {errorMessage}";
        }

        /// <summary>
        /// <see cref="IServiceResponseStatus.Status"/>.
        /// </summary>
        public override ResponseStatus Status { get; }

        public override string ToString()
        {
            return
                $"CorrelationId: {CorrelationId}{Environment.NewLine}" +
                $"TimeStamp: {TimeStamp}{Environment.NewLine}" +
                $"Result: {Status.ToString()}{Environment.NewLine}" +
                $"Error: {Message}{Environment.NewLine}" +
                $"StackTrace: {Exception}";
        }
    }
}