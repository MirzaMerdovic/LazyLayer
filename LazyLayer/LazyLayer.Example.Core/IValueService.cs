using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LazyLayer.Example.Core
{
    public interface IValueService
    {
        Task<int> CreateAsync(Value value);

        Task<IEnumerable<Value>> GetAsync();

        Task<Value> GetAsync(int id);

        Task Update(Value value);

        Task Delete(int id);
    }
}