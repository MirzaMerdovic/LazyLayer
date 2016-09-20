using System;

namespace LazyLayer.Core.Contracts
{
    /// <summary>
    /// Represents successful service response that returns a content.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OkResponse<T> : BaseContentResponse<T>
    {
        /// <summary>
        /// <see cref="IResponseStatus.Status"/>
        /// </summary>
        public override ResponseStatus Status => ResponseStatus.Found;

        /// <summary>
        /// Initializes new instance of <see cref="OkResponse"/>
        /// </summary>
        /// <param name="correlationId"><see cref="BaseResponse.CorrelationId"/>.</param>
        /// <param name="content">Response content. Usually a query result.</param>
        public OkResponse(Guid correlationId, T content) : base(correlationId, content)
        {
        }
    }
}