using HealthCare.Descriptions.Application.Common.Extensions.ExpressionExtensions;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Abstracts;
using System.Linq.Expressions;

namespace HealthCare.Descriptions.Application.Features.Wrappers.Helpers
{
    public class TokenBasedPaginationHelper<T, THandler, TDto, TPropType> : ITokenBasedPaginationHelper<T, THandler, TDto, TPropType>
        where T : class
        where THandler : class
    {
        private readonly IDecryptionHelper _decryptionHelper;
        private readonly IEncryptionHelper _encryptionHelper;

        public TokenBasedPaginationHelper(IDecryptionHelper decryptionHelper, IEncryptionHelper encryptionHelper)
        {
            _decryptionHelper = decryptionHelper;
            _encryptionHelper = encryptionHelper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<TDto>>>
            PaginationResultAsync(TokenPayloadConfig<T, THandler, TDto, TPropType> config, DBQueryOptions<T>? dBQueryOptions = null)
        {
            if (dBQueryOptions == null)
            {
                dBQueryOptions = new DBQueryOptions<T>();
            }

            CursorTokenPayload<TPropType, THandler> tokenPayload = new CursorTokenPayload<TPropType, THandler>(); // Sonraki sayfa için gerekli bilgiler.

            if (string.IsNullOrEmpty(config.Token))
            {
                // Paging Token yoksa toplam veriyi bir kez olmak üzere bulur.
                // FilterQuery'den gelen filtreye göre toplam sayıyı bulmak için dBQueryOptions eklenir. Aksi halde tüm kayıt sayısını verir.
                tokenPayload.TotalCount = await config.GetTotalCountAsync(dBQueryOptions);
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
                CryptionResponse<CursorTokenPayload<TPropType, THandler>> cryptionResponse =
                    _decryptionHelper.DecryptToken<CursorTokenPayload<TPropType, THandler>>(config.Token);


                isForward = cryptionResponse.TokenPayload.IsForward;

                Expression<Func<T, bool>> mainFilter = dBQueryOptions.filter;

                // ileri ve Geri yönlü sayfalamadaki filtre ve sıralama yapısı.
                if (cryptionResponse.TokenPayload.IsForward) // İleri yönlü
                {
                    // Extension sınıf ile yeni filtre ekler.
                    mainFilter = (mainFilter == null) ? config.ForwardFilter(cryptionResponse.TokenPayload.LastData) :
                        mainFilter.And(config.ForwardFilter(cryptionResponse.TokenPayload.LastData));
                }
                else
                {
                    // Extension sınıf ile yeni filtre ekler.
                    mainFilter = (mainFilter == null) ? config.BackwardFilter(cryptionResponse.TokenPayload.FirstData) :
                        mainFilter.And(config.BackwardFilter(cryptionResponse.TokenPayload.FirstData));

                    dBQueryOptions.sortingType = 1;
                }

                dBQueryOptions.filter = mainFilter;
            }

            InternalServiceResponse<IReadOnlyCollection<TDto>> serviceResult = await config.FetchDataAsync(dBQueryOptions);

            tokenPayload.IsForward = isForward;

            // Örneğin 10 veri almak istenirse 11 tane veri getir sorgusu yazılır. Eğer 11 veri dönerse en az bir sayfa daha veri var demektir.
            // Eğer 10 veya daha az dönerse son sayfada olunduğu anlaşılır.
            if (serviceResult.Data.Count > tokenPayload.TakenCount)
            {
                isLastPage = false;
                List<TDto> resultList = serviceResult.Data.ToList();
                resultList.RemoveAt(serviceResult.Data.Count - 1);
                serviceResult.Data = resultList;

                tokenPayload.FirstData = config.CursorSelector(serviceResult.Data.FirstOrDefault());
                tokenPayload.LastData = config.CursorSelector(serviceResult.Data.LastOrDefault());
                tokenPayload.LastCreatedAt = config.CreatedAtSelector(serviceResult.Data.LastOrDefault());

                pagingToken = _encryptionHelper.EncryptToken(tokenPayload);
            }
            else
            {
                isLastPage = true;
            }

            if (!isForward)
            {
                serviceResult.Data = serviceResult.Data.Reverse().ToList();
            }

            return serviceResult.ToHandlerResponse(pagingToken, isLastPage);
        }
    }
}