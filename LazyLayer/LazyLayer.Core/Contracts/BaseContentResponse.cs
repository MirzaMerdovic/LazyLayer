using System;

namespace LazyLayer.Core.Contracts
{
    /// <summary>
    /// Represent base service result that needs to be implemented when creating service result that returns content.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseContentResponse<T> : BaseResponse
    {
        /// <summary>
        /// Response content. Usually a query result.
        /// </summary>
        public T Content { get; }

        /// <summary>
        /// Initializes new instance of <see cref="BaseContentResponse{T}"/>.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="content"></param>
        protected BaseContentResponse(Guid correlationId, T content) : base(correlationId)
        {
            Content = content;
        }
    }
}