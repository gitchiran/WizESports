using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;

namespace Wiz_eSports_Management.Common
{
    public static class SessionExtensions
    {
        public static void SetSession(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetSession<T>(this ISession session, string key)
        {
            return JsonConvert.DeserializeObject<T>(session.GetString(key));
        }

        public static T DeepClone<T>(this T a)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

                serializer.Serialize(stream, a);
                stream.Position = 0;
                return (T)serializer.Deserialize(stream);
            }
        }
    }
}
