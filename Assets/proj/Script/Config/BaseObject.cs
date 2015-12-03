using System;
using System.Xml.Serialization;

namespace cavallaro.tesi.smartedifice
{
    [Serializable]
    public class BaseObject
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Position")]
        public string Position { get; set; }

        [XmlAttribute("Rotation")]
        public string Rotation { get; set; }

        [XmlAttribute("Dimension")]
        public string Dimension { get; set; }

    }
}
