using UnityEngine;

namespace cavallaro.tesi.smartedifice
{
    public class SmartHeater : SmartObject
    {
        Material fire;
        Color defaultColor;

        void Start()
        {
            fire = this.GetComponent<Renderer>().materials[1];
            defaultColor = fire.color;
        }

        protected override void OFF()
        {
            fire.color = defaultColor;
        }

        protected override void ON()
        {
            fire.color = Color.red;
        }
    }
}
