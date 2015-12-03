using UnityEngine;

namespace cavallaro.tesi.smartedifice
{
    public class SmartFan : SmartObject
    {
        private GameObject elica;
        private AudioSource audioSource;
        private bool rotate = false;
        private float intensity = 100;

        public void Start()
        {
            elica = GameObject.Find("Elica");
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (rotate)
            {
                elica.transform.Rotate(Vector3.right, intensity);
            }
        }

        protected override void OFF()
        {
            rotate = false;
            audioSource.Stop();
        }

        protected override void ON()
        {
            intensity = 100 * settedValue;
            rotate = true;

            float normalizedVal = (settedValue - 1) / (4 - 1);
            audioSource.volume = normalizedVal;
            audioSource.Play();
        }

    }
}
