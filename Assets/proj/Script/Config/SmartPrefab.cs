using System;
using System.Xml.Serialization;
using UnityEngine;

namespace cavallaro.tesi.smartedifice
{
    [Serializable]
    public class SmartPrefab : Prefab
    {
        [XmlAttribute("Script")]
        public string Script { get; set; }

        [XmlAttribute("GUID")]
        public string GUID { get; set; }

        public override GameObject Render()
        {
            GameObject element = base.Render();
            if(element != null)
            {
                SmartObject smart = element.GetComponent<SmartObject>();
                if(smart != null)
                {
                    smart.GUID = this.GUID;
                }
            }

            return element;
        }
    }
}
