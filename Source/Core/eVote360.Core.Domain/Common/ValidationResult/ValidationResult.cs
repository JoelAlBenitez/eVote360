using eVote360.Core.Domain.Common.Errors;

namespace eVote360.Core.Domain.Common.ValidationResult
{
    public sealed class ValidationResult
    {
        public IReadOnlyCollection<Error> errors { get; }
        public bool IsValid => errors.Count == 0;
        public ValidationResult(IReadOnlyCollection<Error> errors) {
        
            this.errors= errors;
        }
        public static ValidationResult Success()
            => new([]);

        public static ValidationResult Failure(params Error[] errors)
            => new (errors);

    }
}
