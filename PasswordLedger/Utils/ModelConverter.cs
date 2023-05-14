using PasswordLedger.Model;
using System.Text.Json;

namespace PasswordLedger.Utils
{
    class ModelConverter
    {
        public static string ToJSONString(Credential credentials)
        {
            string rData = "{";
            rData += "\"Title\" : \"" + credentials.Title + "\", ";
            rData += "\"Username\" : \"" + credentials.Username + "\", ";
            rData += "\"Password\" : \"" + credentials.Password + "\", ";
            rData += "\"Extended\" : \"" + credentials.Extended + "\"";
            rData += "}";
            return rData;
        }

        public static string ToString(Credential credential)
        {
            string rData = "";
            rData += "Title : " + credential.Title + " ";
            rData += "Username : " + credential.Username + " ";
            rData += "Password : " + credential.Password + " ";
            rData += "Extended : " + credential.Extended;
            return rData;
        }

        public static Credential JSONToCredential(string json)
        {
            Credential credential = new Credential();
            
            JsonDocument jsonDoc = JsonDocument.Parse(json);
            foreach(JsonProperty property in jsonDoc.RootElement.EnumerateObject())
            {
                switch(property.Name)
                {
                    case "Title":
                        credential.Title = property.Value.ToString();
                        break;
                    case "Username":
                        credential.Username = property.Value.ToString();
                        break;
                    case "Password":
                        credential.Password = property.Value.ToString();
                        break;
                    case "Extended":
                        credential.Extended = property.Value.ToString();
                        break;
                }
            }

            return credential;
        }

        public static List<Credential> JSONToListCredential(string json)
        {
            List<Credential> list = new List<Credential>();

            JsonDocument jsonDoc = JsonDocument.Parse(json);
            JsonElement vals = jsonDoc.RootElement.GetProperty("vals");

            foreach(JsonElement Obj in vals.EnumerateArray())
                list.Add(JSONToCredential(Obj.ToString()));

            return list;
        }

        public static string CredentialListToJSON(List<Credential> credentials)
        {
            string vals = "{ \"vals\" : [";

            for(int i = 0; i < credentials.Count; i++)
            {
                Credential cred = credentials[i];
                vals += ModelConverter.ToJSONString(cred);
                if(i + 1 < credentials.Count)
                {
                    vals += ", ";
                }
            }

            vals += "]}";
            return vals;
        }
    }
}
