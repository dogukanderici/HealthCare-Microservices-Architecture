using Core.WorkflowEngine.Application.Commons.Extensions.ExpressionExtensions;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
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
            if (options == null)
                options = new DBQueryOptions<T>();

            PaginationPayload<TPropType> paginationPayload = new PaginationPayload<TPropType>();

            if (string.IsNullOrEmpty(config.Token))
            {
                // Paging token yoksa bir seferlik toplam veriyi bulur.
                paginationPayload.TotalCount = await config.GetTotalCountAsync(options);
                paginationPayload.TakenCount = config.Take;
            }

            // Son sayfada olup olunmadığını anlamak için N+1 kuralı uygulanır.
            // Take sayısının bir fazlası kadar istek atılır. N+1 kadar veri gelirse en az bir sayfa daha veri var demektir.
            options.DataTakeNumber = paginationPayload.TakenCount + 1;

            options.orderBy = config.OrderBy;
            options.sortingType = 0;

            bool isForward = true;
            bool isLastPage = false;
            string pagingToken = "";

            if (!string.IsNullOrEmpty(config.Token))
            {
                // İstekten gelen token bilgisi çözülür.
                CryptionResponse<PaginationPayload<TPropType>> cryptionResponse = _decryptionHelper.DecryptToken<PaginationPayload<TPropType>>(config.Token);

                isForward = cryptionResponse.TokenPayload.IsForward;

                Expression<Func<T, bool>> mainFilter = options.filter;

                // İleri ve Geri yönlü fitreleme yapabilmesi için.
                if (isForward) // İleri yönlü
                {
                    mainFilter = (mainFilter == null) ? config.ForwardFilter(cryptionResponse.TokenPayload.LastData) :
                        mainFilter.And(config.ForwardFilter(cryptionResponse.TokenPayload.LastData));
                }
                else
                {
                    mainFilter = (mainFilter == null) ? config.BackwardFilter(cryptionResponse.TokenPayload.FirstData) :
                                            mainFilter.And(config.BackwardFilter(cryptionResponse.TokenPayload.FirstData));

                    options.sortingType = 1;
                }

                options.filter = mainFilter;
            }

            InternalServiceResponse<IReadOnlyCollection<TDto>> serviceResult = await config.FetchDataAsync(options);

            paginationPayload.IsForward = isForward;

            // Son sayfa kontrolü
            if (serviceResult.Data.Count > paginationPayload.TakenCount)
            {
                isLastPage = false;

                List<TDto> resultList = serviceResult.Data.ToList();
                resultList.RemoveAt(serviceResult.Data.Count - 1);
                serviceResult.Data = resultList;

                paginationPayload.FirstData = config.CursorSelector(serviceResult.Data.FirstOrDefault());
                paginationPayload.LastData = config.CursorSelector(serviceResult.Data.LastOrDefault());
                paginationPayload.LastCreatedAt = config.CreatedAtCursor(serviceResult.Data.LastOrDefault());

                pagingToken = _encryptionHelper.EncryptToken(paginationPayload);
            }
            else
            {
                isLastPage = true;
            }

            if (!isForward)
                serviceResult.Data = serviceResult.Data.Reverse().ToList();

            return serviceResult.ToHandlerResponse(pagingToken, isLastPage);
        }
    }
}