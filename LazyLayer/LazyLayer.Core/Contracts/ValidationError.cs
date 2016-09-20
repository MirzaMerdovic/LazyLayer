using System.Collections.Generic;

namespace LazyLayer.Core.Contracts
{
    public class ValidationError
    {
        public string PropertyName { get; set; }

        public List<string> Errors { get; set; } = new List<string>(0); 
    }
}