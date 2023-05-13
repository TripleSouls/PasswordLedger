using PasswordLedger.Model;
namespace PasswordLedger.Utils
{
    class ModelConverter
    {
        public static string ToJSONString(Credentials credentials)
        {
            string rData = "{";
            rData += "\"Title\" : \"" + credentials.Title + "\", ";
            rData += "\"Username\" : \"" + credentials.Username + "\", ";
            rData += "\"Password\" : \"" + credentials.Password + "\", ";
            rData += "\"Extended\" : \"" + credentials.Extended + "\"";
            rData += "}";
            return rData;
        }

        public static string ToString(Credentials credentials)
        {
            string rData = "";
            rData += "Title : " + credentials.Title + " ";
            rData += "Username : " + credentials.Username + " ";
            rData += "Password : " + credentials.Password + " ";
            rData += "Extended : " + credentials.Extended;
            return rData;
        }
    }
}
