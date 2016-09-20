using System;

namespace LazyLayer.Core.Contracts
{
    /// <summary>
    /// Represents service execution failure.
    /// </summary>
    public class FailedResponse : BaseResponse
    {
        /// <summary>
        /// <see cref="Exception"/> that has been thrown.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Initializes new instance of <see cref="FailedResponse"/>.
        /// </summary>
        /// <param name="correlationId"><see cref="BaseResponse.CorrelationId"/>.</param>
        /// <param name="exception"><see cref="Exception"/>.</param>
        /// <param name="methodName">Method that caused the failure.</param>
        public FailedResponse(Guid correlationId, Exception exception, string methodName) : base(correlationId)
        {
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
            Message = $"{methodName} failed with error: {errorMessage}";
        }

        /// <summary>
        /// <see cref="IResponseStatus.Status"/>.
        /// </summary>
        public override ResponseStatus Status => ResponseStatus.Failure;

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