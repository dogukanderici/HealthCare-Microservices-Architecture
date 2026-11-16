using Core.WebUI.Application.Commons.Parameters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Core.WebUI.Application.Commons.Parameters.UINotificaitonEnums;

namespace Core.AdminUI.Commons.Extensions
{
    public static class TempDataExtension
    {
        public const string NotificationKey = "UINotification";
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        public static void SetNotification<T>(
            this ITempDataDictionary tempData,
            UIApiResponse<T> apiResponse,
            NotificationType notificationType,
            string userMessage,
            string title = null)
        {
            UINotificationPayload payload = new UINotificationPayload
            {
                Type = notificationType,
                StatusCode = (int)apiResponse.StatusCode,
                TechnicalMessage = apiResponse.Message,
                Title = title ?? GetDefaultTitle(notificationType),
                UserMessage = userMessage
            };

            tempData[NotificationKey] = JsonSerializer.Serialize(payload, _jsonOptions);
        }
        private static string GetDefaultTitle(NotificationType type) => type switch
        {
            NotificationType.Success => "Başarılı",
            NotificationType.Warning => "Uyarı",
            NotificationType.Info => "Bilgi",
            _ => "Hata"
        };
    }
}