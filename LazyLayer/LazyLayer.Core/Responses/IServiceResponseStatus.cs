using System;

namespace LazyLayer.Core.Responses
{
    /// <summary>
    /// Interface that every service execution result needs to inherit.
    /// </summary>
    public interface IServiceResponseStatus
    {
        /// <summary>
        /// Indicator that tells the status of the result, for example in <see cref="FailedResponse"/> it will be Failure.
        /// </summary>
        ResponseStatus Status { get; }

        /// <summary>
        /// <see cref="Guid"/> value that will be used as a correlation identifier for tracking purposes.
        /// </summary>
        Guid CorrelationId { get; }

        /// <summary>
        /// Represents date and time of response creation.
        /// </summary>
        DateTime TimeStamp { get; }

        /// <summary>
        /// Gets or sets response message that user can see.
        /// </summary>
        string Message { get; set; }
    }
}