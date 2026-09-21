using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Behaviors
{
    public class TokenDecryptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest, IPagedQueryBase
        where TResponse : IInternalHandlerResponse
    {
        private readonly IDecryptionHelper _decryptionHelper;

        public TokenDecryptionBehavior(IDecryptionHelper decryptionHelper)
        {
            _decryptionHelper = decryptionHelper;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Token))
            {
                return await next();
            }

            CryptionResponse<string> decryptionResult = _decryptionHelper.DecryptTokenForString(request.Token);
            bool isValidToken = decryptionResult.IsSuccess;

            if (!isValidToken)
            {
                // Token Geçersizse
                throw new Exception(decryptionResult.Message);
            }
            else
            {
                // Token Geçerliyse
            }

            return await next();
        }
    }
}