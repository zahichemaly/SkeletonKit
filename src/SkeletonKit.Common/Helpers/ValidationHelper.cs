using CME.Common.Exceptions;
using FluentValidation;
using ValidationException = CME.Common.Exceptions.ValidationErrorException;

namespace CME.Common.Helpers
{
    /// <summary>
    /// Used to manually validate a model.
    /// </summary>
    public class ValidationHelper<T>
    {
        private IValidator<T> _validator;

        public ValidationHelper(IValidator<T> validator)
        {
            _validator = validator;
        }

        public void Validate(T model)
        {
            var validationResult = _validator.Validate(model);

            if (validationResult is not null && !validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors?.Select(x => new ErrorDetails()
                {
                    Code = x.ErrorCode,
                    Message = x.ErrorMessage,
                }) ?? new List<ErrorDetails>();

                throw new ValidationException(validationErrors);
            };
        }
    }
}
