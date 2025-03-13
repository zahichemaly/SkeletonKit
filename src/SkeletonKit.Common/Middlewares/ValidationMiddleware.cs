using CME.Common.Exceptions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ValidationException = CME.Common.Exceptions.ValidationErrorException;

namespace CME.Common.Middlewares
{
    internal class ValidationMiddleware : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                List<ErrorDetails> errorDetails = new List<ErrorDetails>();

                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(g => g.Key, g => g.Value.Errors);

                foreach (var errorInModelState in errorsInModelState)
                {
                    var code = $"invalid_{errorInModelState.Key.ToLowerInvariant()}";
                    foreach (var error in errorInModelState.Value)
                    {
                        var message = error.ErrorMessage;
                        errorDetails.Add(new ErrorDetails()
                        {
                            Code = code,
                            Message = message
                        });
                    }
                }

                throw new ValidationException(errorDetails);
            }
            await next();
        }
    }

    public static class ValidationMiddlewareExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services, Assembly assembly)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(assembly);
            services.AddControllers(option => option.Filters.Add<ValidationMiddleware>());
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            return services;
        }
    }
}
