using System;
using System.Threading.Tasks;

namespace LazyLayer.Core.Services
{
    public class FuncTaskExecutor : IExecutor<object>
    {
        private readonly Func<Task> _action;

        public FuncTaskExecutor(Func<Task> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public string MethodName => _action.Method.Name;

        public bool IsVoid => true;

        public Task<object> Run()
        {
            _action();

            return Task.FromResult<object>(null);
        }
    }
}