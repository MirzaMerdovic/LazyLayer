namespace LazyLayer.Core.Contracts
{
    /// <summary>
    /// Interface that every service execution result needs to inherit.
    /// </summary>
    public interface IResponseStatus
    {
        /// <summary>
        /// Indicator that tells the status of the result, for example in <see cref="FailedResponse"/> it will be Failure.
        /// </summary>
        ResponseStatus Status { get; }

        /// <summary>
        /// Gets or sets response message that user can see.
        /// </summary>
        string Message { get; set; }
    }
}