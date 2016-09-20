using System;
using System.Threading.Tasks;
using LazyLayer.Core.Contracts;
using LazyLayer.Core.Providers;

namespace LazyLayer.Core.Services
{
    /// <summary>
    /// Orchestrates service requests and responses between upper layers and service layer.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IServiceDispatcher<TResponse>
    {
        ILogProvider Logger { get; }

        /// <summary>
        /// Executes specified method and returns converted response that calling layer understands.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="action">Method from service layer that will be executed.</param>
        /// <returns>Instance of <see cref="TResponse"/>.</returns>
        Task<TResponse> ExecuteAsync(ServiceRequest request, Func<Task> action);

        /// <summary>
        /// Executes specified method and returns converted response that calling layer understands.
        /// </summary>
        /// <typeparam name="TContent">Type of request's content.</typeparam>
        /// <param name="request"></param>
        /// <param name="action">Method from service layer that will be executed.</param>
        /// <returns>Instance of <see cref="TResponse"/>.</returns>
        Task<TResponse> ExecuteAsync<TContent>(ServiceRequest<TContent> request, Func<TContent, Task> action);

        /// <summary>
        /// Executes specified method and returns converted response that calling layer understands.
        /// </summary>
        /// <typeparam name="TResult">Type of <see cref="Contracts.BaseResponse"/>.</typeparam>
        /// <param name="request"></param>
        /// <param name="action">Method from service layer that will be executed.</param>
        /// <returns></returns>
        Task<TResponse> ExecuteAsync<TResult>(ServiceRequest request, Func<Task<TResult>> action);

        /// <summary>
        /// Executes specified method and returns converted response that calling layer understands.
        /// </summary>
        /// <typeparam name="TContent">Content type.</typeparam>
        /// <typeparam name="TResult">Type of <see cref="Contracts.BaseResponse"/>.</typeparam>
        /// <param name="request"></param>
        /// <param name="action">Method from service layer that will be executed.</param>
        /// <returns>Instance of <see cref="TResponse"/>.</returns>
        Task<TResponse> ExecuteAsync<TContent, TResult>(ServiceRequest<TContent> request, Func<TContent, Task<TResult>> action);
    }
}