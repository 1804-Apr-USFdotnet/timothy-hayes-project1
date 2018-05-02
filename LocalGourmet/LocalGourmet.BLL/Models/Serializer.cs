using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace LocalGourmet.BLL.Models
{
    // Static class for Serialization and Deserialization
    public static class Serializer
    {
        // Serialize object to JSON string.
        // Return JSON string
        public static string Serialize<T>(T obj)
        {
            string jsonStr = "";

            MemoryStream ms = new MemoryStream();
            try
            {
                DataContractJsonSerializer ser = 
                    new DataContractJsonSerializer(typeof(T));
                ser.WriteObject(ms, obj);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                jsonStr = sr.ReadToEnd();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ms.Close();
            }
            return jsonStr;
        }

        // Deserialize JSON string and return object.
        public static T Deserialize<T>(string jsonStr)
        {
            T obj = default(T);
            MemoryStream ms = new MemoryStream();
            try
            {
                DataContractJsonSerializer ser = 
                    new DataContractJsonSerializer(typeof(T));
                StreamWriter writer = new StreamWriter(ms);
                writer.Write(jsonStr);
                writer.Flush();
                ms.Position = 0;
                obj = (T)ser.ReadObject(ms); 
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ms.Close();
            }
            return obj;
        }
    }
}
