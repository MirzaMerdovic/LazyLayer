using System;

namespace LazyLayer.Core.Responses
{
    /// <summary>
    /// Interface that every service execution result needs to inherit.
    /// </summary>
    public interface IServiceResponse
    {
        /// <summary>
        /// Gets the flag that determines whether the response has content or not (void)
        /// </summary>
        bool HasContent { get; }

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

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        Exception Ex {get;set;}
    }
}