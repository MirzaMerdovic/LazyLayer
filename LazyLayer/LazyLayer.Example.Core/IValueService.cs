using System.Threading.Tasks;

namespace LazyLayer.Example.Core
{
    public interface IValueService
    {
        Task CreateAsync(Value value);

        Task<Value> GetAsync();

        Task<Value> GetAsync(int id);
    }
}