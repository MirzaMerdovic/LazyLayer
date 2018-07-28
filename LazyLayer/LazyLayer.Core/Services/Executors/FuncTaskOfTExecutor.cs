using System;
using System.Threading.Tasks;

namespace LazyLayer.Core.Services
{
    public class FuncTaskOfTExecutor<T> : IExecutor<T>
    {
        private readonly Func<Task<T>> _action;

        public FuncTaskOfTExecutor(Func<Task<T>> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public string MethodName => _action.Method.Name;

        public bool IsVoid => false;

        public Task<T> Run()
        {
            return _action();
        }
    }
}