namespace LazyLayer.Core.Responses
{
    /// <summary>
    /// Represents possible result of service executions.
    /// </summary>
    public enum ResponseStatus
    {
        /// <summary>
        /// Unknown result means that call hasn't finished executing and erroneous behavior hasn't been handled properly,
        /// because of some unpredicted scenario.
        /// </summary>
        Unknown,

        /// <summary>
        /// Execution finished successfully.
        /// </summary>
        Success,

        /// <summary>
        /// One or many results found.
        /// </summary>
        Found,

        /// <summary>
        /// Entity not found in database.
        /// </summary>
        NotFound,

        /// <summary>
        /// Execution failed. Check response from error information.
        /// </summary>
        Failure
    }
}