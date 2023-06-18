using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Web.App.Framework
{
    public class ConvertTransactions
    {
        public static T ConvertToJsonObject<T>(string json)
        {
            T data=JsonConvert.DeserializeObject<T>(json);
            return data;
        }

        public static string ConvertToObjectJson<T>(T data)
        {
            string json=JsonConvert.SerializeObject(data);
            return json;
        }

        public static T DeserializeXml<T>(string xmlData)//Xml i Object yapar
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (TextReader reader = new StringReader(xmlData))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        public static string SerializeToXml<T>(T obj)//Object i xml yapar.
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringWriter writer = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(writer))
                {
                    serializer.Serialize(xmlWriter, obj);
                    return writer.ToString();
                }
            }
        }
    }
}