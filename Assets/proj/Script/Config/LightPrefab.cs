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

            light.transform.position = parseVector3(Position);
            light.transform.rotation = Quaternion.Euler(parseVector3(Rotation));
            light.transform.localScale = parseVector3(Dimension);

        }
    }
}
