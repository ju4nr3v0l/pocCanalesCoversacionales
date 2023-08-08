namespace POCWhatsapp.Util;

public interface IUtil
{
    object textMessaje(string message, string number);
    object ImageMessage(string url, string number);
    object AudioMessage(string url, string number);
    object VideoMessage(string url, string number);
    object DocumentMessage(string url, string number);
    object LocationMessage(string latitude, string longitude, string address, string name, string number);
    object ButtonsMessage(string buttonTitle, string idButton1, string buttonLabel1, string idButton2, string buttonLabel2, string number);
}