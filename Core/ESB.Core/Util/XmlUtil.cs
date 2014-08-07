using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace ESB.Core.Util
{
    public static class XmlUtil
    {
        /// <summary>
        /// 对象到XML-----泛类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SaveXmlFromObj<T>(T obj)
        {
            if (obj == null) return null;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            MemoryStream stream = new MemoryStream();
            XmlTextWriter xtw = new XmlTextWriter(stream, Encoding.UTF8);
            xtw.Formatting = Formatting.Indented;
            try
            {
                serializer.Serialize(stream, obj);
            }
            catch { return null; }

            stream.Position = 0;
            StringBuilder returnStr = new StringBuilder();
            using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    returnStr.AppendLine(line);
                }
            }
            return returnStr.ToString();
        }

        /// <summary>
        /// XML到反序列化到对象----支持泛类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T LoadObjFromXML<T>(string data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(stream, Encoding.UTF8))
                {
                    sw.Write(data);
                    sw.Flush();
                    stream.Seek(0, SeekOrigin.Begin);
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    try
                    {
                        return ((T)serializer.Deserialize(stream));
                    }
                    catch { return default(T); }

                }
            }
        }
    }
}
