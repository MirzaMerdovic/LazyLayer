namespace LazyLayer.Core.Requests
{
    /// <summary>
    /// Represents a service method request that contains content.
    /// </summary>
    /// <typeparam name="T">Content type.</typeparam>
    public class ServiceRequest<T> : ServiceRequest
    {
        /// <summary>
        /// Gets or sets <see cref="ServiceRequest{T}"/> content.
        /// </summary>
        public T Content { get; }

        /// <summary>
        /// Initializes new instance of <see cref="ServiceRequest{T}"/>.
        /// </summary>
        /// <param name="content"></param>
        public ServiceRequest(T content)
        {
            Content = content;
        }
    }
}