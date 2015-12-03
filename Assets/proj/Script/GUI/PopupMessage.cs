using UnityEngine;
using System.Collections;
using System;

namespace cavallaro.tesi.smartedifice
{
    public class PopupMessage : MonoBehaviour
    {
        private bool showText = false;
        private string Guimessage;
        private GUIStyle style = new GUIStyle();

        private ArrayList list = new ArrayList();

        void Start()
        {
            style.fontSize = 25;
        }

        public void showMessage(string message)
        {
            list.Add(message);
        }

        void Update()
        {
            if(list.Count != 0 && !showText)
            {
                String msg = (String)list[0];
                list.RemoveAt(0);

                StartCoroutine(ShowMessage(msg, 2));
            }
        }
        

        IEnumerator ShowMessage(string message, float delay)
        {
            Guimessage = message;
            showText = true;
            yield return new WaitForSeconds(delay);
            showText = false;
        }

        void OnGUI()
        {
            if (showText)
                GUI.Label(new Rect(0, 0, 100, 100), Guimessage, style);
        }
    }
}
