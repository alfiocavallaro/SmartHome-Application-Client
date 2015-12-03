using UnityEngine;

namespace cavallaro.tesi.smartedifice
{
    [RequireComponent(typeof(AudioSource))]
    public class SmartStereo : SmartObject
    {

        protected override void ON()
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.volume = settedValue / 10;
            audio.Play();
        }

        protected override void OFF()
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Stop();
        }
    }
}



