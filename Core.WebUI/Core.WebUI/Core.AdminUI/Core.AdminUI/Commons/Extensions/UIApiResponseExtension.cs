using Core.WebUI.Application.Commons.Parameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using static Core.WebUI.Application.Commons.Parameters.UINotificaitonEnums;

namespace Core.AdminUI.Commons.Extensions
{
    public static class UIApiResponseExtension
    {
        /// <summary>
        /// API yanıtının durumuna göre ilgili yönlendirmeyi (Redirect) veya görünümü (View) döndürür. TempData opsiyonel parametre olup değer verilmesi halinde çalışır.
        /// </summary>
        /// <typeparam name="T">API yanıtının içerisindeki Data tipi</typeparam>
        /// <param name="response">Extension sınıfı yazılan nesne</param>
        /// <param name="redirect">İşlem sonucu başarılı olduğunda yönlendirelecek hedef (Redirect).</param>
        /// <param name="view">İşlem sonucu başarısız olduğunda döndürülecek sayfa görünümü (View).</param>
        /// <param name="tempData">Başarı veya hata mesajlarının taşınacağı TempData sözlüğü (Opsiyonel).</param>
        /// <returns>İşlem başarılıysa <paramref name="redirect"/>, başarısızsa <paramref name="view"/> sonucunu <see cref="IActionResult"/> olarak döndürür.</returns>
        public static IActionResult ReturnControllerResult<T>(this UIApiResponse<T> response, RedirectResult redirect, ViewResult view, ITempDataDictionary? tempData = null)
        {
            if (response.Status)
            {
                SuccessTempData(response, tempData);
                return redirect;
            }

            ErrorTempData(response, tempData);
            return view;
        }

        /// <summary>
        /// API yanıtının durumuna göre ilgili yönlendirmeyi (Redirect) veya görünümü (View) döndürür. TempData opsiyonel parametre olup değer verilmesi halinde çalışır.
        /// </summary>
        /// <typeparam name="T">API yanıtının içerisindeki Data tipi</typeparam>
        /// <param name="response">Extension sınıfı yazılan nesne</param> 
        /// <param name="view">İşlem sonucu başarılı olduğunda döndürülecek sayfa görünümü (View).</param>
        /// <param name="redirect">İşlem sonucu başarısız olduğunda yönlendirelecek hedef (Redirect).</param>
        /// <param name="tempData">Başarı veya hata mesajlarının taşınacağı TempData sözlüğü (Opsiyonel).</param>
        /// <returns>İşlem başarılıysa <paramref name="view"/>, başarısızsa <paramref name="redirect"/> sonucunu <see cref="IActionResult"/> olarak döndürür.</returns>
        public static IActionResult ReturnControllerResult<T>(this UIApiResponse<T> response, ViewResult view, RedirectResult redirect, ITempDataDictionary? tempData = null)
        {
            if (response.Status)
            {
                SuccessTempData(response, tempData);
                return view;
            }

            ErrorTempData(response, tempData);
            return redirect;
        }

        /// <summary>
        /// API yanıtının durumuna bakmaksızın ilgili yönlendirmeyi (Redirect) yapar. TempData opsiyonel parametre olup değer verilmesi halinde çalışır.
        /// </summary>
        /// <typeparam name="T">API yanıtının içerisindeki Data tipi</typeparam>
        /// <param name="response">Extension sınıfı yazılan nesne</param>
        /// <param name="redirect">İşlem sonucu başarılı veya başarısız olduğunda yönlendirelecek hedef (Redirect).</param>
        /// <param name="tempData">Başarı veya hata mesajlarının taşınacağı TempData sözlüğü (Opsiyonel).</param>
        /// <returns>İşlem başarılıysa veya başarısızsa <paramref name="redirect"/> sonucunu <see cref="IActionResult"/> olarak döndürür.</returns>
        public static IActionResult ReturnControllerResult<T>(this UIApiResponse<T> response, RedirectResult redirect, ITempDataDictionary? tempData = null)
        {
            if (response.Status)
                SuccessTempData(response, tempData);
            else
                ErrorTempData(response, tempData);

            return redirect;
        }

        private static void SuccessTempData<T>(this UIApiResponse<T> response, ITempDataDictionary? tempData = null)
        {
            if (tempData != null)
            {
                tempData.SetNotification(response, NotificationType.Success, "İşlem Başarılı");
            }
        }

        private static void ErrorTempData<T>(this UIApiResponse<T> response, ITempDataDictionary? tempData = null)
        {
            if (tempData != null)
            {
                tempData.SetNotification(response, NotificationType.Error, "Bir Hata Olouştu!");
            }
        }
    }
}