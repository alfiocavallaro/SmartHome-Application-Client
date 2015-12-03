using UnityEngine;
using System.Xml.Serialization;
using System;
using System.IO;
using System.Text;

namespace cavallaro.tesi.smartedifice
{
    public class Parser
    {
        public static bool Serialize<T>(T value, String filename)
        {
            if (value == null)
            {
                return false;
            }

            Stream stream = new FileStream(filename, FileMode.Create);

            try
            {
                XmlSerializer _xmlserializer = new XmlSerializer(typeof(T), new Type[] {
                    typeof(BaseObject),
                    typeof(LightPrefab),
                    typeof(Prefab),
                    typeof(SmartPrefab),
                    });

                StreamWriter streamWrite = new StreamWriter(stream, Encoding.UTF8);

                _xmlserializer.Serialize(streamWrite, value);

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                return false;
            }
            finally
            {
                stream.Close();
            }
        }


        public static T Deserialize<T>(String filename)
        {
            if (string.IsNullOrEmpty(filename) || !File.Exists(filename))
            {
                return default(T);
            }

            Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);

            try
            {
                XmlSerializer _xmlserializer = new XmlSerializer(typeof(T), new Type[] {
                    typeof(BaseObject),
                    typeof(LightPrefab),
                    typeof(Prefab),
                    typeof(SmartPrefab),
                    });

                var result = (T)_xmlserializer.Deserialize(stream);
                return result;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                return default(T);
            }
            finally
            {
                stream.Close();
            }

        }

    }
}