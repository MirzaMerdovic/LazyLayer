using System;

namespace LazyLayer.Core.Responses
{
    /// <summary>
    /// Represent base service result that contain basic properties needed for other concrete service result implementations.
    /// </summary>
    public abstract class ServiceResponse : IServiceResponseStatus
    {
        /// <summary>
        /// Represents date and time of response creation.
        /// </summary>
        public DateTime TimeStamp => DateTime.UtcNow;

        /// <summary>
        /// <see cref="Guid"/> value that will be used as a correlation identifier for tracking purposes.
        /// </summary>
        public Guid CorrelationId { get; }

        /// <summary>
        /// <see cref="IServiceResponseStatus.Status"/>.
        /// </summary>
        public abstract ResponseStatus Status { get; }

        /// <summary>
        /// <see cref="IServiceResponseStatus.Message"/>.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="ServiceResponse"/>.
        /// </summary>
        /// <param name="correlationId"></param>
        protected ServiceResponse(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
    }
}