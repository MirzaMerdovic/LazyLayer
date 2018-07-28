using System;
using System.Threading.Tasks;

namespace LazyLayer.Core.Services
{
    public class FuncOfTTaskExecutor<T> : IExecutor<object>
    {
        private readonly T _arg;
        private readonly Func<T, Task> _action;

        public FuncOfTTaskExecutor(T arg, Func<T, Task> action)
        {
            _arg = arg;
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public string MethodName => _action.Method.Name;

        public bool IsVoid => true;

        public Task<object> Run()
        {
            _action(_arg);

            return Task.FromResult<object>(null);
        }
    }
}