using System;

namespace LazyLayer.Core.Responses
{
    /// <summary>
    /// Represent base service result that needs to be implemented when creating service result that returns content.
    /// </summary>
    /// <typeparam name="T">Content type.</typeparam>
    public abstract class ServiceResponse<T> : ServiceResponse
    {
        /// <summary>
        /// Response content. Usually a query result.
        /// </summary>
        public T Content { get; }

        /// <summary>
        /// Initializes new instance of <see cref="ServiceResponse{T}"/>.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="content"></param>
        protected ServiceResponse(Guid correlationId, T content) : base(correlationId)
        {
            Content = content;
        }
    }
}