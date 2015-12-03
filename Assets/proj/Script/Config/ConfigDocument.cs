using System.Xml.Serialization;
using System;
using System.Collections.Generic;

namespace cavallaro.tesi.smartedifice
{
    [Serializable, XmlRoot("Scene")]
    public class ConfigDocument
    {
        [XmlArray("Light")]
        [XmlArrayItem("Item")]
        public List<LightPrefab> Lights { get; set; }

        [XmlArray("Prefab")]
        [XmlArrayItem("Item")]
        public List<Prefab> Prefabs { get; set; }

        [XmlArray("SmartObject")]
        [XmlArrayItem("Item")]
        public List<SmartPrefab> SmartPrefab { get; set; }
    }
}
