using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration.Constants;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Commons.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ITransactionalRequest
        where TResponse : IInternalCommandResponse, new()
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

        public TransactionBehavior(IUnitOfWork unitOfWork, ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {

                // CommandHandler Sınıfında Yazılan Kodlar Çalıştırılır.
                TResponse response = await next();

                await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    typeof(TRequest).Name,
                    LogConstants.SuccessMessages.TransactionSuccessed);

                return new TResponse
                {
                    IsSuccess = true,
                    InternalMessage = LogConstants.SuccessMessages.TransactionSuccessed
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();

                _logger.LogError(LogConstants.LogMessageTemplate,
                    typeof(TRequest).Name,
                    ex);

                return new TResponse
                {
                    IsSuccess = false,
                    InternalMessage = LogConstants.ErrorMessages.TransactionFailed
                };
            }
        }
    }
}