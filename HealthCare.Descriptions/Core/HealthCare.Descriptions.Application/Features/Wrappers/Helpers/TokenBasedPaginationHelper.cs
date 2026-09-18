using HealthCare.Descriptions.Application.Common.Helpers;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Domain.Abstracts;

namespace HealthCare.Descriptions.Application.Features.Wrappers.Helpers
{
    public static class TokenBasedPaginationHelper
    {
        public static async Task<InternalHandlerResponse<IReadOnlyCollection<TDto>>>
            PaginationResult<T, THandler, TDto, TPropType>(TokenPayloadConfig<T, THandler, TDto, TPropType> config)
            where T : class, IEntity
            where THandler : class
        {
            DBQueryOptions<T> dBQueryOptions = new DBQueryOptions<T>();
            CursorTokenPayload<TPropType, THandler> tokenPayload = new CursorTokenPayload<TPropType, THandler>(); // Sonraki sayfa için gerekli bilgiler.

            if (string.IsNullOrEmpty(config.Token))
            {
                // Paging Token yoksa toplam veriyi bir kez olmak üzere bulur.
                tokenPayload.TotalCount = await config.GetTotalCountAsync();
                tokenPayload.TakenCount = config.Take;
            }

            // Son sayfada olup olunmadığını anlamak için N+1 taktiği uygulanır.
            dBQueryOptions.DataTakeNumber = tokenPayload.TakenCount + 1;

            dBQueryOptions.orderBy = config.OrderBy;
            dBQueryOptions.sortingType = 0;

            bool isForward = true;
            bool isLastPage = false;
            string pagingToken = "";

            if (!string.IsNullOrEmpty(config.Token))
            {
                // İstekten gelen token bilgisi çözülür.
                CursorTokenPayload<TPropType, THandler> cursorTokenPayload =
                    DecryptionHelper.DecryptToken<CursorTokenPayload<TPropType, THandler>>(config.Token, config.SecretKey);

                isForward = cursorTokenPayload.IsForward;

                // ileri ve Geri yönlü sayfalamadaki filtre ve sıralama yapısı.
                if (cursorTokenPayload.IsForward) // İleri yönlü
                {
                    dBQueryOptions.filter = config.ForwardFilter(cursorTokenPayload.LastData);
                }
                else
                {
                    dBQueryOptions.filter = config.BackwardFilter(cursorTokenPayload.FirstData);
                    dBQueryOptions.sortingType = 1;
                }
            }

            InternalServiceResponse<IReadOnlyCollection<TDto>> serviceResult = await config.FetchDataAsync(dBQueryOptions);

            tokenPayload.IsForward = isForward;
            tokenPayload.FirstData = config.CursorSelector(serviceResult.Data.First());

            // Örneğin 10 veri almak istenirse 11 tane veri getir sorgusu yazılır. Eğer 11 veri dönerse en az bir sayfa daha veri var demektir.
            // Eğer 10 veya daha az dönerse son sayfada olunduğu anlaşılır.
            if (serviceResult.Data.Count > tokenPayload.TakenCount)
            {
                isLastPage = false;
                List<TDto> resultList = serviceResult.Data.ToList();
                resultList.RemoveAt(serviceResult.Data.Count - 1);
                serviceResult.Data = resultList;

                tokenPayload.LastData = config.CursorSelector(serviceResult.Data.Last());
                tokenPayload.LastCreatedAt = config.CreatedAtSelector(serviceResult.Data.Last());

                pagingToken = EncryptionHelper.EncryptToken(tokenPayload, config.SecretKey);

            }
            else
            {
                isLastPage = false;
            }

            if (!isForward)
            {
                serviceResult.Data = serviceResult.Data.Reverse().ToList();
            }

            return serviceResult.ToHandlerResponse(pagingToken, isLastPage);
        }
    }
}