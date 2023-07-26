using System.Text;

namespace Persistence.Helpers;

public static class ConvertHelper
{
    public static string ConvertLoginToBase64(string username,string password)
    {
        string creds = string.Format("{0}:{1}", username, password);
        byte[] bytes = Encoding.ASCII.GetBytes(creds);
        return System.Convert.ToBase64String(bytes);
    }



    public static string ConvertToBase64(string str)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(str);
        return System.Convert.ToBase64String(plainTextBytes);
    }



    public static string ConvertFromBase64(string str)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(str);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}