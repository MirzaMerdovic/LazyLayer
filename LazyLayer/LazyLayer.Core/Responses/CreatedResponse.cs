using System;

namespace LazyLayer.Core.Responses
{
    public class CreatedResponse<T> : ServiceResponse<T>
    {
        public override ResponseStatus Status => ResponseStatus.Created;

        public CreatedResponse(Guid correlationId, T content) : base(correlationId, content)
        {
        }
    }
}