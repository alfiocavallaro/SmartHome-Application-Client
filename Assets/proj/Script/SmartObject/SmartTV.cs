using UnityEngine;

namespace cavallaro.tesi.smartedifice
{
    public class SmartTV : SmartObject
    {
        Renderer rend;
        Material screen;

        Texture onTex1;
        Texture onTex2;
        Texture onTex3;
        Texture blackTex;

        private float exTime;

        System.Random rand = new System.Random();

        // Use this for initialization
        public void Start()
        {
            rend = this.GetComponent<Renderer>();

            screen = rend.materials[1];

            blackTex = Resources.Load("black") as Texture;
            onTex1 = Resources.Load("paesaggio") as Texture;
            onTex2 = Resources.Load("dolomiti") as Texture;
            onTex3 = Resources.Load("barca") as Texture;

            screen.SetTexture("_EmissionMap", blackTex);

        }

        // Update is called once per frame
        void Update()
        {
            if (isOn)
            {
                float time = Time.time;
                if (time - exTime > 2)
                {
                    screen.SetTexture("_EmissionMap", selectTex());
                    exTime = time;
                }
            } 
        }

        
        protected override void OFF()
        {
            screen.SetTexture("_EmissionMap", blackTex);
            isOn = false;
            
        }

        protected override void ON()
        {
            screen.SetTexture("_EmissionMap", selectTex());
            isOn = true;
        }

        Texture selectTex()
        {
            int val = rand.Next(0, 3);

            if (val == 1) return onTex1;
            else if (val == 2) return onTex2;
            else return onTex3;
        }
    }

}

