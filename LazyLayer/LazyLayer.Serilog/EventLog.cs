using System;

namespace LazyLayer.Serilog
{
    /// <summary>
    /// Represents EventLog table used by SeriLog.
    /// </summary>
    public class EventLog
    {
        /// <summary>
        /// Gets or sets unique identifier of <see cref="EventLog"/>.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets log correlation identifier, used for tracking purposes.
        /// </summary>
        public Guid CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets log message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets log message template.
        /// </summary>
        public string MessageTemplate { get; set; }

        /// <summary>
        /// Gets or sets log level. This is a string representation of <see cref="LogLevel"/> enumeration.
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// Gets or sets log creation date.
        /// </summary>
        public DateTimeOffset TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets exception data if any.
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// Gets or sets message template's parameters.
        /// </summary>
        public string Properties { get; set; }

        /// <summary>
        /// Gets or sets log event value.
        /// </summary>
        public string LogEvent { get; set; }
    }
}