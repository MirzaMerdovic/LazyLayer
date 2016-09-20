using System.Collections.Generic;
using System.Linq;
using LazyLayer.Core.Contracts;

namespace LazyLayer.Core.Providers
{
    public class NullValidationProvider : IValidationProvider
    {
        public bool Validate<T>(T content)
        {
            return true;
        }

        public IEnumerable<ValidationError> GetErrors()
        {
            return Enumerable.Empty<ValidationError>();
        }
    }
}