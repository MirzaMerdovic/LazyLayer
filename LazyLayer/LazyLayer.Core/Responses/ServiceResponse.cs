using System;

namespace LazyLayer.Core.Responses
{
    /// <summary>
    /// Represent base service result that contain basic properties needed for other concrete service result implementations.
    /// </summary>
    public class ServiceResponse : IServiceResponse
    {
        /// <summary>
        /// <see cref="IServiceResponse.HasContent"/>
        /// </summary>
        public bool HasContent { get; }

        /// <summary>
        /// <see cref="IServiceResponse.TimeStamp"/>
        /// </summary>
        public DateTime TimeStamp => DateTime.UtcNow;

        /// <summary>
        /// <see cref="IServiceResponse.CorrelationId"/>
        /// </summary>
        public Guid CorrelationId { get; }

        /// <summary>
        /// Response content. Usually a query result.
        /// </summary>
        public dynamic Content { get; }

        /// <summary>
        /// Initializes new instance of <see cref="ServiceResponse"/>.
        /// </summary>
        /// <param name="correlationId"></param>
        public ServiceResponse(Guid correlationId, bool hasContent = false, dynamic content = null)
        {
            CorrelationId = correlationId;
            HasContent = hasContent;
            Content = content;
        }

        /// <summary>
        /// <see cref="IServiceResponse.Message"/>.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// <see cref="IServiceResponse.Ex"/>
        /// </summary>
        public Exception Ex { get; set; }
    }
}