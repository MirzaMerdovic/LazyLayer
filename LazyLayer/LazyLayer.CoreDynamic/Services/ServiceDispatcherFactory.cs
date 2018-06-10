using LazyLayer.Core.Providers;
using LazyLayer.Core.Services.Dispatchers;
using System;
using System.Collections.Generic;

namespace LazyLayer.Core.Services
{
    public static class ServiceDispatcherFactory<T>
    {
        public static IServiceDispatcher<T> Create(IResponseConversionProvider<T> convertor)
        {
            return Create(convertor, null);
        }

        public static IServiceDispatcher<T> Create(IResponseConversionProvider<T> convertor, ILogProvider logger)
        {
            return new ServiceDispatcher<T>(convertor.ConvertResponse, logger);
        }
    }
}