using Core.WebUI.Application.Commons.Parameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using static Core.WebUI.Application.Commons.Parameters.UINotificaitonEnums;

namespace Core.AdminUI.Commons.Extensions
{
    public static class UIApiResponseExtension
    {
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