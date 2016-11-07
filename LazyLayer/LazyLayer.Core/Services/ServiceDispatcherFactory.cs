using LazyLayer.Core.Providers;

namespace LazyLayer.Core.Services
{
    public static class ServiceDispatcherFactory<T>
    {
        public static IServiceDispatcher<T> Create(IResponseConversionProvider<T> convertor)
        {
            return new ServiceDispatcher<T>(convertor.ConvertResponse);
        }

        public static IServiceDispatcher<T> Create(IResponseConversionProvider<T> convertor, ILogProvider logger)
        {
            return new ServiceDispatcher<T>(convertor.ConvertResponse, logger);
        }
    }
}