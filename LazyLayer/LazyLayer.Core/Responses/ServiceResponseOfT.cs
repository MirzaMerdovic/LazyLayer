using System;

namespace LazyLayer.Core.Responses
{
    /// <summary>
    /// Represent base service result that needs to be implemented when creating service result that returns content.
    /// </summary>
    /// <typeparam name="T">Content type.</typeparam>
    public abstract class ServiceResponseOfT<T> : ServiceResponse
    {
        /// <summary>
        /// Response content. Usually a query result.
        /// </summary>
        public T Content { get; }

        /// <summary>
        /// Initializes new instance of <see cref="ServiceResponseOfT{T}"/>.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="content"></param>
        protected ServiceResponseOfT(Guid correlationId, T content) : base(correlationId)
        {
            Content = content;
        }
    }
}