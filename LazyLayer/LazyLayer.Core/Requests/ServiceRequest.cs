using System;

namespace LazyLayer.Core.Requests
{
    /// <summary>
    /// Represents a plain service method request.
    /// </summary>
    public class ServiceRequest
    {
        /// <summary>
        /// Gets <see cref="Guid"/> value that will be used as a correlation identifier for tracking purposes.
        /// </summary>
        public Guid CorrelationId => Guid.NewGuid();

        /// <summary>
        /// Gets request creation date.
        /// </summary>
        public DateTime TimeStamp => DateTime.UtcNow;

        /// <summary>
        /// Gets or sets creator of request.
        /// </summary>
        public string UserName { get; set; }

        public override string ToString()
        {
            return $"Correlation Id: {CorrelationId}, TimeStamp: {TimeStamp}, UserName: {UserName}";
        }
    }
}