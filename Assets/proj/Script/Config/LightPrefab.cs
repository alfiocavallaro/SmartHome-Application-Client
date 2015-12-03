using UnityEngine;
using System.Xml.Serialization;
using System;

namespace cavallaro.tesi.smartedifice
{
    [Serializable]
    public class LightPrefab : BaseObject
    {
        
        [XmlAttribute("Intensity")]
        public float Intensity { get; set; }

        [XmlAttribute("Range")]
        public float Range { get; set; }

        [XmlAttribute("typeLight")]
        public LightType TypeLight { get; set; }

        [XmlAttribute("GUID")]
        public string GUID { get; set; }

        public LightPrefab()
        {
            this.Dimension = "1,1,1";
        }

        public void Render()
        {
            GameObject gameObject = new GameObject(this.Name);
            gameObject.AddComponent<Light>();
            SmartLight smartLight = gameObject.AddComponent<SmartLight>();
            Light light = gameObject.GetComponent<Light>();

            light.intensity = Intensity;
            light.range = Range;
            light.type = TypeLight;
            light.renderMode = LightRenderMode.ForcePixel;
            smartLight.GUID = GUID;

            string[] elements = Position.Split(new char[] { ',' });
            Vector3 position = new Vector3(float.Parse(elements[0]), float.Parse(elements[1]), float.Parse(elements[2]));

            elements = Dimension.Split(new char[] { ',' });
            Vector3 dimension = new Vector3(float.Parse(elements[0]), float.Parse(elements[1]), float.Parse(elements[2]));

            elements = Rotation.Split(new char[] { ',' });
            Vector3 rot = new Vector3(float.Parse(elements[0]), float.Parse(elements[1]), float.Parse(elements[2]));
            Quaternion rotation = Quaternion.Euler(rot);

            light.transform.position = position;
            light.transform.rotation = rotation;
            light.transform.localScale = dimension;

        }
    }
}
