using System;
using System.Xml.Serialization;
using UnityEngine;

namespace cavallaro.tesi.smartedifice
{
    [Serializable]
    public class Prefab : BaseObject
    {
        [XmlAttribute("Resource")]
        public string Resource { get; set; }

        public virtual GameObject Render()
        {
            GameObject element = null;
            try
            {
                element = GameObject.Instantiate(Resources.Load(this.Resource)) as GameObject;
                element.name = this.Name;

                element.transform.position = parseVector3(Position);
                element.transform.rotation = Quaternion.Euler(parseVector3(Rotation));
                element.transform.localScale = parseVector3(Dimension);
            }catch(Exception){
                Debug.LogError(string.Format("Impossibile insantiate: {0}. Required prefab: {1}",
                    this.Name, this.Resource));
            }

            return element;
        }

    }
}
