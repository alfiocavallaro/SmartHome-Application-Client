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

                string[] elements = Position.Split(new char[] { ',' });
                Vector3 position = new Vector3(float.Parse(elements[0]), float.Parse(elements[1]), float.Parse(elements[2]));

                elements = Dimension.Split(new char[] { ',' });
                Vector3 dimension = new Vector3(float.Parse(elements[0]), float.Parse(elements[1]), float.Parse(elements[2]));

                elements = Rotation.Split(new char[] { ',' });
                Vector3 rot = new Vector3(float.Parse(elements[0]), float.Parse(elements[1]), float.Parse(elements[2]));
                Quaternion rotation = Quaternion.Euler(rot);

                element.transform.position = position;
                element.transform.rotation = rotation;
                element.transform.localScale = dimension;
            }catch(Exception)
            {
                string msg = string.Format("Impossibile insantiate: {0}. Required prefab: {1}", this.Name, this.Resource);
                Debug.LogError(msg);
            }

            return element;

        }
    }
}
