using System.Threading.Tasks;

namespace LazyLayer.Example.Core
{
    public class ValueService : IValueService
    {
        public Task CreateAsync(Value value)
        {
            return null;
        }

        public Task<Value> GetAsync()
        {
            return Task.FromResult(new Value());
        }

        public Task<Value> GetAsync(int id)
        {
            return Task.FromResult(new Value());
        }
    }
}