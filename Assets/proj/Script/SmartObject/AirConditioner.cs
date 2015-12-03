using UnityEngine;

namespace cavallaro.tesi.smartedifice
{
    public class AirConditioner : SmartObject
    {
        private AudioSource audioSource;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        protected override void OFF()
        {
            audioSource.Stop();
        }

        protected override void ON()
        {
            audioSource.Play();
        }
    }
}
