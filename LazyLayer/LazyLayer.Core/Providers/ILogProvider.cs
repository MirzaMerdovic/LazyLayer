using System;

namespace LazyLayer.Core.Providers
{
    public interface ILogProvider
    {
        void Verbose(Guid correlationId, string messageTemplate, params object[] propertyValues);

        void Debug(Guid correlationId, string messageTemplate, params object[] propertyValues);

        void Information(Guid correlationId, string messageTemplate, params object[] propertyValues);

        void Warning(Guid correlationId, string messageTemplate, params object[] propertyValues);

        void Error(Guid correlationId, string messageTemplate, params object[] propertyValues);

        void Error(Guid correlationId, Exception exception, string messageTemplate, params object[] propertyValues);

        void Fatal(Guid correlationId, string messageTemplate, params object[] propertyValues);

        void Fatal(Guid correlationId, Exception exception, string messageTemplate, params object[] propertyValues);
    }
}