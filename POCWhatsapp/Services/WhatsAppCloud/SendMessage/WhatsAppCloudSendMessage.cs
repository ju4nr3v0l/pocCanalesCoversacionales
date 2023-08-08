using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;


namespace POCWhatsapp.Services.WhatsAppCloud.SendMessage;

public class WhatsAppCloudSendMessage : IWhatsAppCloudSendMessage
{
    public async Task<bool> Execute(object model)
    {
        var client = new HttpClient();
        var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

        using (var content = new ByteArrayContent(byteData))
        {
            string endpoint = "https://graph.facebook.com";
            string phoneNumberId = "112465938599868";
            string version = "v17.0";
            string accessToken = "EAAS3RdcEFIkBO7iz3YFSNyizDcFFQiB3pCAHsMtU9wqDGVW4FsNWKy98kz6TQRtZBlWMZBDGZCE9aFpLJCGC6vjl0FbjHrHfE8G9xQFcQquXKkIcJsa9UBIJDb91DPRxofDpZCpEUO8l1gdA7AhUMcJZBqGePvNbKSE9T4Ir4ztr8EtpCBZAfysY7teLz6iHfIivNJz2r9gZCZBSWUtd";
            string uri = $"{endpoint}/{version}/{phoneNumberId}/messages";

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}