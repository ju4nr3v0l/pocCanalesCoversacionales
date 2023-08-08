namespace POCWhatsapp.Util;

public class Util : IUtil
{
    public object textMessaje(string message, string number)
    {
        return new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = number,
            type = "text",
            text = new
            {
                preview_url = false,
                body = message
            }
        };
    }
    
    public object ImageMessage(string url, string number)
    {
        return new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = number,
            type = "image",
            image = new
            {
                link = url
            }
        };
    }
    
    public object AudioMessage(string url, string number)
    {
        return new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = number,
            type = "audio",
            audio = new
            {
                link = url
            }
        };
    }
    
    public object VideoMessage(string url, string number)
    {
        return new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = number,
            type = "video",
            video = new
            {
                link = url
            }
        };
    }
    
    public object DocumentMessage(string url, string number)
    {
        return new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = number,
            type = "document",
            document = new
            {
                link = url
            }
        };
    }
    
    public object LocationMessage(string latitude, string longitude,string address, string name, string number)
    {
        return new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = number,
            type = "location",
            location = new
            {
                latitude = latitude,
                longitude = longitude,
                address = address,
                name = name
            }
        };
    }
    
    public object ButtonsMessage(string buttonTitle, string idButton1,string buttonLabel1,string idButton2,string buttonLabel2,  string number)
    {
        return new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = number,
            type = "interactive",
            interactive = new
            {
                type = "button",
                body = new
                {
                    text = buttonTitle
                },
                action = new
                {
                    buttons = new List<object>
                    {
                        new
                        {
                            type = "reply",
                            reply = new
                            {
                                id = idButton1,
                                title = buttonLabel1
                            }
                        },
                        new
                        {
                            type = "reply",
                            reply = new
                            {
                                id = idButton2,
                                title = buttonLabel2
                            }
                        }
                    }
                }
            }
        };
    }
}