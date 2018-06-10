using System;

namespace LazyLayer.Core.Providers
{
    public class NullLogProvider : ILogProvider
    {
        public void Verbose(Guid correlationId, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Debug(Guid correlationId, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Information(Guid correlationId, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Warning(Guid correlationId, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Error(Guid correlationId, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Error(Guid correlationId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Fatal(Guid correlationId, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Fatal(Guid correlationId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }
    }
}