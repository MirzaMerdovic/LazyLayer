using System;

namespace LazyLayer.Core.Contracts
{
    /// <summary>
    /// Represents successful service response.
    /// </summary>
    public class OkResponse : BaseResponse
    {
        /// <summary>
        /// <see cref="IResponseStatus.Status"/>.
        /// </summary>
        public override ResponseStatus Status { get; }

        /// <summary>
        /// Initializes new instance of <see cref="OkResponse"/>.
        /// </summary>
        /// <param name="correlationId"><see cref="BaseResponse.CorrelationId"/>.</param>
        public OkResponse(Guid correlationId) : base(correlationId)
        {
            Status = ResponseStatus.Success;
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