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

        public static IServiceDispatcher<T> Create(IResponseConversionProvider<T> convertor, IValidationProvider validator)
        {
            return new ServiceDispatcher<T>(convertor.ConvertResponse, null, validator);
        }

        public static IServiceDispatcher<T> Create(
            IResponseConversionProvider<T> convertor, 
            ILogProvider logger, 
            IValidationProvider validator)
        {
            return new ServiceDispatcher<T>(convertor.ConvertResponse, logger, validator);
        }
    }
}