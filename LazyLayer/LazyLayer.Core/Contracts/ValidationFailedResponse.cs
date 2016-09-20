using System;
using System.Collections.Generic;
using System.Linq;

namespace LazyLayer.Core.Contracts
{
    public class ValidationFailedResponse : BaseResponse
    {
        public override ResponseStatus Status => ResponseStatus.ValidationError;

        public List<ValidationError> Errors { get; set; }

        public ValidationFailedResponse(Guid correlationId, IEnumerable<ValidationError> errors) : base(correlationId)
        {
            Errors = errors.ToList();
        }
    }
}