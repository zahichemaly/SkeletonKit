namespace SkeletonKit.Common.Exceptions
{
    public sealed class ValidationErrorException : Exception
    {
        public IEnumerable<ErrorDetails> Errors { get; set; }

        public ValidationErrorException(IEnumerable<ErrorDetails> errors)
        {
            this.Errors = errors;
        }
    }
}
