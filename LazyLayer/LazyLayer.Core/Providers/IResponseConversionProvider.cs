using LazyLayer.Core.Responses;

namespace LazyLayer.Core.Providers
{
    public interface IResponseConversionProvider<out TConvertedResponse>
    {
        TConvertedResponse ConvertResponse(IServiceResponseStatus response);
    }
}