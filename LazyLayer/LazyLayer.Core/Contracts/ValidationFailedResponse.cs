using System;
using System.Collections.Generic;
using System.Linq;

namespace LazyLayer.Core.Contracts
{
    public class ValidationFailedResponse : BaseResponse
    {
        public override ResponseStatus Status {get;}

        public List<ValidationError> Errors { get; set; }

        public ValidationFailedResponse(Guid correlationId, IEnumerable<ValidationError> errors) : base(correlationId)
        {
            Status = ResponseStatus.ValidationError;
            Errors = errors.ToList();
        }
    }
}