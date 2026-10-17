using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Helpers
{
    public class TokenBasedPaginationHelper<T, TPropType, TDto> : ITokenBasedPaginationHelper<T, TPropType, TDto>
            where T : class, IEntity
    {
        private readonly IDecryptionHelper _decryptionHelper;
        private readonly IEncryptionHelper _encryptionHelper;

        public TokenBasedPaginationHelper(IDecryptionHelper decryptionHelper, IEncryptionHelper encryptionHelper)
        {
            _decryptionHelper = decryptionHelper;
            _encryptionHelper = encryptionHelper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<TDto>>> PaginationAsync(PaginationConfig<T, TPropType, TDto> config, DBQueryOptions<T>? options = null)
        {
            throw new NotImplementedException();
        }
    }
}