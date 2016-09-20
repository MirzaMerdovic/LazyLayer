using System;

namespace LazyLayer.Core.Contracts
{
    /// <summary>
    /// Represents response that tells that resource could not be found..
    /// </summary>
    public class NotFoundResponse : BaseResponse
    {
        /// <summary>
        /// Resource's unique identifier.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// <see cref="IResponseStatus.Status"/>.
        /// </summary>
        public override ResponseStatus Status => ResponseStatus.NotFound;

        /// <summary>
        /// Initializes new instance of <see cref="NotFoundResponse"/>.
        /// </summary>
        /// <param name="correlationId"><see cref="BaseResponse.CorrelationId"/>.</param>
        /// <param name="id"><see cref="Id"/>.</param>
        public NotFoundResponse(Guid correlationId, int id) : base(correlationId)
        {
            Id = id;
            Message = $"Entity: {id} does not exist!";
        }

        public override string ToString()
        {
            return
                $"CorrelationId: {CorrelationId}{Environment.NewLine}" +
                $"TimeStamp: {TimeStamp}{Environment.NewLine}" +
                $"Result: {Status.ToString()}{Environment.NewLine}" +
                $"Error: {Message}{Environment.NewLine}";
        }
    }
}