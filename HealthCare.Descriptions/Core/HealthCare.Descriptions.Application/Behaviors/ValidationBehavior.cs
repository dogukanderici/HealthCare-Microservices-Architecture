using FluentValidation;
using HealthCare.Descriptions.Application.Common.CustomExceptions;
using HealthCare.Descriptions.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static HealthCare.Descriptions.Application.Common.Constants.ExceptionConstants;

namespace HealthCare.Descriptions.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IValidationRequest
        where TResponse : IValidationResult
    {
        private readonly IEnumerable<IValidator<TRequest>> _validator;
        //private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validator)
        {
            _validator = validator;
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
                    // Log işlemleri middleware içerisinde yapılıyor.
                    // Global Exception Middleware ile validasyona özel cevap döndürülür.
                    throw new ValidationRuleException($"{ValidationExMessage} {request}", failures);
                }
            }

            return await next();
        }
    }
}