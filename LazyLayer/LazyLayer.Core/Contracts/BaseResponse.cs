using System;

namespace LazyLayer.Core.Contracts
{
    /// <summary>
    /// Represent base service result that contain basic properties needed for other concrete service result implementations.
    /// </summary>
    public abstract class BaseResponse : IResponseStatus
    {
        /// <summary>
        /// <see cref="IResponseStatus.Message"/>.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Represents date and time of response creation.
        /// </summary>
        public DateTime TimeStamp { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// <see cref="Guid"/> value that will be used as a correlation identifier for tracking purposes.
        /// </summary>
        public Guid CorrelationId { get; private set; }

        /// <summary>
        /// Initializes new instance of <see cref="BaseResponse"/>.
        /// </summary>
        /// <param name="correlationId"></param>
        protected BaseResponse(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        /// <summary>
        /// <see cref="IResponseStatus.Status"/>.
        /// </summary>
        public abstract ResponseStatus Status { get; }
    }
}