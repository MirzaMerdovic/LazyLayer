using System;

namespace LazyLayer.Http
{
    /// <summary>
    /// Represents error information that can be shown to user.
    /// </summary>
    public class ErrorInfo
    {
        /// <summary>
        /// Gets or sets error date and time.
        /// </summary>
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets URI, Web API rout that has failed to complete.
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// <see cref="Guid"/> value that represents correlation identifier that can be used for tracking purposes.
        /// </summary>
        /// <remarks>This property should have the same value as <see cref="Core.Contracts.ServiceRequest.CorrelationId"/>.</remarks>
        public Guid ErrorId { get; set; }

        public static ErrorInfo Create(string message, string requestUrl, Guid errorId)
        {
            return new ErrorInfo
            {
                Message = message,
                RequestUrl = requestUrl,
                ErrorId = errorId
            };
        }
    }
}