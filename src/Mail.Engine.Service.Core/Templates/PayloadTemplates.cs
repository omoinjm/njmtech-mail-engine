using System.Text.Json;
using Mail.Engine.Service.Core.Helpers;

namespace Mail.Engine.Service.Core.Templates
{
    public static class PayloadTemplates
    {
        public static string SendConfirmationMessage(string message)
        {
            var payload = new
            {
                template_name = "response_message_v16",
                broadcast_name = "response_message_v16_broadcast",
                parameters = new[]
                    {
                        new { name = "notificationmessage", value = $"{message}" }
                    }
            };

            return JsonSerializer.Serialize(payload);
        }

        public static string SendLoginTemplate(string id)
        {
            var payload = new
            {
                template_name = "user_login_v5",
                broadcast_name = "user_login_v5_broadcast",
                parameters = new[]
                {
                    new { name = "loginkey", value = $"{EncryptionHelper.Encrypt(id)}" }
                }
            };

            return JsonSerializer.Serialize(payload);
        }
    }
}
