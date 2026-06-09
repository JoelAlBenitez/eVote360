using eVote360.Core.Domain.Common.Errors;

namespace eVote360.Core.Domain.Common.ValidationResult
{
    public class ValidationResult
    {
        public IReadOnlyCollection<Error> errors { get; }
        public bool IsValid => errors.Count == 0;
        public ValidationResult(IReadOnlyCollection<Error> errors) {
        
            this.errors= errors;
        }
        public static ValidationResult Success()
            => new([]);

        public static ValidationResult Failure(List<Error> errors1, params Error[] errors)
            => new (errors);

    }

    public class ValidationResult<T> : ValidationResult
    {

        public  T? Value { get; }

        public ValidationResult(T value, IReadOnlyCollection<Error> errors) : base(errors)
        {
            Value = value;
        }


        public static ValidationResult<T> Success(T value)
            => new ValidationResult<T>(value, null!);

        public static  ValidationResult<T> Failure(List<Error> errors1, params Error[] errors)
            => new ValidationResult<T>(default!, errors);
    }
}
