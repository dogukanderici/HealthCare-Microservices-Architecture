using FluentValidation;
using HealthCare.Descriptions.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IValidationRequest
        where TResponse : IValidationResult
    {
        private readonly IEnumerable<IValidator<TRequest>> _validator;
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validator, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _validator = validator;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator.Any())
            {
                ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll( // Doğrulama işlemlerinin aynı anda çağırır ve hepsinin bitmesini bekler.
                        _validator.Select(y => y.ValidateAsync(context, cancellationToken))
                    );

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .Select(m => m.ErrorMessage)
                    .ToList();

                if (failures.Count() > 0)
                {
                    _logger.LogError("VALIDATION FAILED. REQUEST: {@Request}, ERRORS: {@Errors}", request, failures);

                    var response = TResponse.WithValidationErrors(failures);

                    return (TResponse)response;

                    // TODO - Global Exception Middleware ile throw yapısına güncellenecek.
                }
            }

            return await next();
        }
    }
}