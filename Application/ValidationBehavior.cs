using FluentValidation;
using MediatR;
using Microsoft.Build.Tasks;

namespace Application
{
    public class ValidationPipeline<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TResponse : class
        where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest> _compositeValidator;

        public ValidationPipeline(IValidator<TRequest> compositeValidator)
        {
            _compositeValidator = compositeValidator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var result = await _compositeValidator.ValidateAsync(request, cancellationToken);

            if (!result.IsValid)
            {
                Error error = new Error();
                var responseType = typeof(TResponse);

                foreach (var validationFailure in result.Errors)
                {
                    error.Reasons.Add(new Error(validationFailure.ErrorMessage));
                }
                // This always returns null instead of a Result with errors in it. 
                var f = Result.Fail(error) as TResponse;
                return f;

                // This causes an exception saying it cannot find the constructor.
                var invalidResponse =
                        Activator.CreateInstance(invalidResponseType, null) as TResponse;

            }

            return await next();
        }
    }
}
