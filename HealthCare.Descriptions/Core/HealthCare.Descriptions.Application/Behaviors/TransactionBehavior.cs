using HealthCare.Descriptions.Application.Common.Constants;
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
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ITransactionalRequest
        where TResponse : IInternalHandlerResponse, new()
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

        public TransactionBehavior(IUnitofWork unitOfWork, ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Handler sınıflarındaki kodlar çalıştırılır.
                TResponse response = await next();

                await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation(LogConstant.MessageTemplate,
                    typeof(TRequest).Name,
                    LogConstant.SuccessMessages.TransactionSuccessed
                    );

                return response;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();

                _logger.LogError(LogConstant.MessageTemplate,
                    typeof(TRequest).Name,
                    ex
                    );

                return new TResponse()
                {
                    IsSuccess = false,
                    InternalMessage = ex.Message
                };
            }
        }
    }
}