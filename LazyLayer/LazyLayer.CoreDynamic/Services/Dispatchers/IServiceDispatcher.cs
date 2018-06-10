using LazyLayer.Core.Requests;
using System.Threading.Tasks;

namespace LazyLayer.Core.Services.Dispatchers
{
    /// <summary>
    /// Orchestrates service requests and responses between upper layers and service layer.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IServiceDispatcher<TResponse> : IServiceDispatcher
    {
        Task<TResponse> Execute(ServiceRequest request, IExecutor executor);
    }

    public interface IServiceDispatcher { }
}