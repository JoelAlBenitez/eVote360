using eVote360.Core.Domain.Common.Errors;
using System.Collections.Generic;
using System.Linq;

namespace eVote360.Core.Domain.Common.ValidationResult
{
    public class ValidationResult
    {
        public IReadOnlyCollection<Error> errors { get; }
        public bool IsValid => errors == null || errors.Count == 0;
        
        public ValidationResult(IReadOnlyCollection<Error> errors) {
            this.errors = errors ?? new List<Error>();
        }
        
        public static ValidationResult Success()
            => new ValidationResult(new List<Error>());

        public static ValidationResult Failure(params Error[] errors)
            => new ValidationResult(errors);
            
        public static ValidationResult Failure(List<Error> errors)
            => new ValidationResult(errors);
    }

    public class ValidationResult<T> : ValidationResult
    {
        public T? Value { get; }

        public ValidationResult(T? value, IReadOnlyCollection<Error> errors) : base(errors)
        {
            Value = value;
        }

        public static ValidationResult<T> Success(T value)
            => new ValidationResult<T>(value, new List<Error>());

        public static new ValidationResult<T> Failure(params Error[] errors)
            => new ValidationResult<T>(default, errors);
            
        public static new ValidationResult<T> Failure(List<Error> errors)
            => new ValidationResult<T>(default, errors);
    }
}
