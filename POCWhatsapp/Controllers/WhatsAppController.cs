using Microsoft.AspNetCore.Mvc;
using POCWhatsapp.Services.WhatsAppCloud.SendMessage;
using POCWhatsapp.Util;

namespace POCWhatsapp.Controllers;

[ApiController]
[Route("api/whatsapp")]
public class WhatsAppController : Controller
{
    private readonly IWhatsAppCloudSendMessage _whatsAppCloudSendMessage;
    private readonly IUtil _util;
    public WhatsAppController(IWhatsAppCloudSendMessage whatsAppCloudSendMessage, IUtil util)
    {
        _whatsAppCloudSendMessage = whatsAppCloudSendMessage;
        _util = util;
    }
    
    [HttpGet("test")]
    public async Task<IActionResult> Sample()
    {

        var data = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = "573185668227",
            type = "text",
            text = new
            {
                preview_url = false,
                body = "Mensaje enviado desde api de marulo"
            }
        };
        var result = await _whatsAppCloudSendMessage.Execute(data);
        return Ok("ok Sample");
    }
    
    [HttpGet]
    public IActionResult VerifyToken()
    {
        // Token creado por nosotros
        string AccessToken = "TokenDeMarulo";
        
        
        var token = Request.Query["hub.verify_token"].ToString();
        var challenge = Request.Query["hub.challenge"].ToString();

        if (challenge != null && token != null && token == AccessToken)
        {
            return Ok(challenge);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> ReceiveMessage([FromBody] WhatsAppCloudModel body)
    {
        try
        {
            var Message = body.Entry[0]?.Changes[0]?.Value?.Messages[0];
            if(Message != null)
            {
                var userNumber = Message.From;
                var userText = GetUserText(Message);
                object objectMessage;
                switch (userText.ToUpper())
                {
                    case "TEXT":
                        objectMessage = _util.textMessaje("Esta es la respuesta a mensaje", userNumber);
                        break;
                    case "IMAGE":
                        objectMessage = _util.ImageMessage("https://biostoragecloud.blob.core.windows.net/resource-udemy-whatsapp-node/image_whatsapp.png", userNumber);
                        break;
                    case "VIDEO":
                        objectMessage = _util.VideoMessage("https://biostoragecloud.blob.core.windows.net/resource-udemy-whatsapp-node/video_whatsapp.mp4", userNumber);
                        break;
                    case "DOCUMENT":
                        objectMessage = _util.DocumentMessage("https://biostoragecloud.blob.core.windows.net/resource-udemy-whatsapp-node/document_whatsapp.pdf",userNumber);
                        break;
                    
                    case "LOCATION":
                        objectMessage = _util.LocationMessage("6.1838352220265795","-75.58590941737309","Cl. 26 Sur # 48 - 91, Zona 1, Envigado, Antioquia","Sistecredito",userNumber);
                        break;
                    default:
                        objectMessage = _util.textMessaje("Esta es la respuesta a default", userNumber);
                        break;
                }
                await _whatsAppCloudSendMessage.Execute(objectMessage);
            }

            //Obligatorio devolver esto
            return Ok("EVENT_RECEIVED");
        }
        catch (Exception ex)
        {
            //Obligatorio devolver esto
            return Ok("EVENT_RECEIVED");
        }
    }

    private string GetUserText(Message message)
    {
        string TypeMessage = message.Type;

        if (TypeMessage.ToUpper() == "TEXT")
        {
            return message.Text.Body;
        }
        else if (TypeMessage.ToUpper() == "INTERACTIVE")
        {
            string interactiveType = message.Interactive.Type;
            if (interactiveType.ToUpper() == "LIST_REPLY")
            {
                return message.Interactive.List_Reply.Title;
            }
            else if (interactiveType.ToUpper() == "BUTTON_REPLY")
            {
                return message.Interactive.Button_Reply.Title;
            }
            else
            {
                return string.Empty;
            }
        }
        else
        {
            return string.Empty;
        }
    }
}