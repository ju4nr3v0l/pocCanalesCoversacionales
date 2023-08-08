namespace POCWhatsapp.Services.WhatsAppCloud.SendMessage;

public interface IWhatsAppCloudSendMessage
{
    Task<bool> Execute(object model);
}