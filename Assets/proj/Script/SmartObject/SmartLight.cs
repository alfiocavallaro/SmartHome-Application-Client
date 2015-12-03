using UnityEngine;

namespace cavallaro.tesi.smartedifice
{
    public class SmartLight : SmartObject
    {
        private Light lightPoint;

        void Start()
        {
            lightPoint = gameObject.GetComponentInChildren<Light>();
        }

        protected override void ON()
        {
            lightPoint.enabled = true;
        }

        protected override void OFF()
        {
            lightPoint.enabled = false;
        }
    }
}
