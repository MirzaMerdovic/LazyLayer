using System.Collections.Generic;
using LazyLayer.Core.Contracts;

namespace LazyLayer.Core.Providers
{
    public interface IValidationProvider
    {
        bool Validate<T>(T content);
        IEnumerable<ValidationError> GetErrors();
    }
}