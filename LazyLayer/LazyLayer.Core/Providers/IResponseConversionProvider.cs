using LazyLayer.Core.Contracts;

namespace LazyLayer.Core.Providers
{
    public interface IResponseConversionProvider<out TConvertedResponse>
    {
        TConvertedResponse ConvertResponse(IResponseStatus response);
    }
}