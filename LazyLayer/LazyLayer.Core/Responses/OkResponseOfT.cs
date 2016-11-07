using System;

namespace LazyLayer.Core.Responses
{
    /// <summary>
    /// Represents successful service response that returns a content.
    /// </summary>
    /// <typeparam name="T">Content type.</typeparam>
    public class OkResponse<T> : ServiceResponseOfT<T>
    {
        /// <summary>
        /// <see cref="IServiceResponseStatus.Status"/>
        /// </summary>
        public override ResponseStatus Status { get; }

        /// <summary>
        /// Initializes new instance of <see cref="OkResponse"/>
        /// </summary>
        /// <param name="correlationId"><see cref="ServiceResponse.CorrelationId"/>.</param>
        /// <param name="content">Response content. Usually a query result.</param>
        public OkResponse(Guid correlationId, T content) : base(correlationId, content)
        {
            Status = ResponseStatus.Found;
        }
    }
}