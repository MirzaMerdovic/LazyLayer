using System;
using System.Threading.Tasks;
using LazyLayer.Core.Contracts;

namespace LazyLayer.Core.Services
{
    public interface IServiceExecutor
    {
        Task<BaseResponse> Invoke(ServiceRequest request, Func<Task> method);
        
        Task<BaseResponse> Invoke<TContent>(ServiceRequest<TContent> request, Func<TContent, Task> method);
        
        Task<BaseResponse> Invoke<TResult>(ServiceRequest request, Func<Task<TResult>> method);
        
        Task<BaseResponse> Invoke<TContent, TResult>(ServiceRequest<TContent> request, Func<TContent, Task<TResult>> method);
    }
}