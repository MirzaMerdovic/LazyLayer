using System.Threading.Tasks;

namespace LazyLayer.Core.Services
{
    public interface IExecutor<T> : IExecutor
    {
        Task<T> Run();
    }

    public interface IExecutor
    {
        string MethodName { get; }

        bool IsVoid { get; }
    }
}