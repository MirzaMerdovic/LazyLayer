using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using LazyLayer.Core.Providers;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace LazyLayer.Serilog
{
    /// <summary>
    /// Implementation of https://serilog.net/ logging.
    /// </summary>
    public class SerilogProvider : ILogProvider
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="Log"/>.
        /// </summary>
        public SerilogProvider()
        {
            var columnOptions = new ColumnOptions
            {
                AdditionalDataColumns = new Collection<DataColumn>
                {
                    new DataColumn {DataType = typeof (Guid), ColumnName = "CorrelationId"}
                }
            };

            var section = (SerilogSection)ConfigurationManager.GetSection("SerilogSettings");

            var config = new LoggerConfiguration();

            if (!string.IsNullOrWhiteSpace(section.SqlServer.ConnectionString))
            {
                config.WriteTo.MSSqlServer(
                    section.SqlServer.ConnectionString,
                    section.SqlServer.TableName,
                    columnOptions: columnOptions);
            }

            if (!string.IsNullOrWhiteSpace(section.RollingFile.LogPath))
            {
                config.WriteTo.RollingFile(Path.Combine(section.RollingFile.LogPath));
            }
                    
             _logger = config.CreateLogger();
        }

        public void Verbose(Guid correlationId, string messageTemplate, params object[] propertyValues)
        {
            if (!_logger.IsEnabled(LogEventLevel.Verbose))
                return;

            messageTemplate = PrepareMessageTemplate(messageTemplate);
            var parameters = PrepareMessageParameters(correlationId, propertyValues);

            _logger.Verbose(messageTemplate, parameters);
        }

        public void Debug(Guid correlationId, string messageTemplate, params object[] propertyValues)
        {
            if (!_logger.IsEnabled(LogEventLevel.Debug))
                return;

            messageTemplate = PrepareMessageTemplate(messageTemplate);
            var parameters = PrepareMessageParameters(correlationId, propertyValues);

            _logger.Debug(messageTemplate, parameters);
        }

        public void Information(Guid correlationId, string messageTemplate, params object[] propertyValues)
        {
            if (!_logger.IsEnabled(LogEventLevel.Information))
                return;

            messageTemplate = PrepareMessageTemplate(messageTemplate);
            var parameters = PrepareMessageParameters(correlationId, propertyValues);

            _logger.Information(messageTemplate, parameters);
        }

        public void Warning(Guid correlationId, string messageTemplate, params object[] propertyValues)
        {
            if (!_logger.IsEnabled(LogEventLevel.Warning))
                return;

            messageTemplate = PrepareMessageTemplate(messageTemplate);
            var parameters = PrepareMessageParameters(correlationId, propertyValues);

            _logger.Warning(messageTemplate, parameters);
        }

        public void Error(Guid correlationId, string messageTemplate, params object[] propertyValues)
        {
            if (!_logger.IsEnabled(LogEventLevel.Error))
                return;

            messageTemplate = PrepareMessageTemplate(messageTemplate);
            var parameters = PrepareMessageParameters(correlationId, propertyValues);

            _logger.Error(messageTemplate, parameters);
        }

        public void Error(Guid correlationId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            if (!_logger.IsEnabled(LogEventLevel.Error))
                return;

            messageTemplate = PrepareMessageTemplate(messageTemplate);
            var parameters = PrepareMessageParameters(correlationId, propertyValues);

            _logger.Error(exception, messageTemplate, parameters);
        }

        public void Fatal(Guid correlationId, string messageTemplate, params object[] propertyValues)
        {
            if (!_logger.IsEnabled(LogEventLevel.Fatal))
                return;

            messageTemplate = PrepareMessageTemplate(messageTemplate);
            var parameters = PrepareMessageParameters(correlationId, propertyValues);

            _logger.Fatal(messageTemplate, parameters);
        }

        public void Fatal(Guid correlationId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            if (!_logger.IsEnabled(LogEventLevel.Fatal))
                return;

            messageTemplate = PrepareMessageTemplate(messageTemplate);
            var parameters = PrepareMessageParameters(correlationId, propertyValues);

            _logger.Fatal(exception, messageTemplate, parameters);
        }

        #region Helpers

        private static string PrepareMessageTemplate(string messageTemplate)
        {
            messageTemplate = "CorrelationId: {CorrelationId}. " + messageTemplate;

            return messageTemplate;
        }

        private static object[] PrepareMessageParameters(Guid correlationId, object[] propertyValues)
        {
            var size = propertyValues.Length + 1;
            var parameters = new object[size];
            parameters[0] = correlationId;

            for (var i = 0; i < propertyValues.Length; i++)
            {
                parameters[i + 1] = propertyValues[i];
            }

            return parameters;
        }

        #endregion
    }
}
