using System;
using System.Threading.Tasks;

namespace LazyLayer.Core.Services
{
    public class FuncOfTTaskOfTExecutor<U, T> : IExecutor<T>
    {
        private readonly U _arg;
        private Func<U, Task<T>> _action;

        public FuncOfTTaskOfTExecutor(U arg, Func<U, Task<T>> action)
        {
            _arg = arg;
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public string MethodName => _action.Method.Name;

        public bool IsVoid => false;

        public Task<T> Run()
        {
            return _action(_arg);
        }
    }
}