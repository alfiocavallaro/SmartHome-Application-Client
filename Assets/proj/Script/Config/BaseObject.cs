using System;
using System.Xml.Serialization;
using UnityEngine;

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

        protected Vector3 parseVector3(string vector)
        {
            string[] elements = vector.Split(new char[] { ',' });
            Vector3 vect = new Vector3(float.Parse(elements[0]), float.Parse(elements[1]), float.Parse(elements[2]));
            return vect;
        }

    }
}
