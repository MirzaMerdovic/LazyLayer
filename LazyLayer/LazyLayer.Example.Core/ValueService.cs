using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LazyLayer.Example.Core
{
    public class ValueService : IValueService
    {
        public Task<int> CreateAsync(Value value)
        {
            return Task.FromResult(value.Id);
        }

        public Task<IEnumerable<Value>> GetAsync()
        {
            return Task.FromResult(Enumerable.Empty<Value>());
        }

        public Task<Value> GetAsync(int id)
        {
            if (id == -1)
                return Task.FromResult<Value>(null);

            return Task.FromResult(new Value() { Id = id });
        }

        public Task Update(Value value)
        {
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
    }
}