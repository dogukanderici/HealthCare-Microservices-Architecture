using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.WebUI.Application.Commons.Parameters.UINotificaitonEnums;

namespace Core.WebUI.Application.Commons.Parameters
{
    public class UINotificationPayload
    {
        public NotificationType Type { get; set; } = NotificationType.Info;
        public int StatusCode { get; set; }
        public string TechnicalMessage { get; set; }
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;
        public string Title { get; set; }
        public string UserMessage { get; set; }
    }
}