using UnityEngine;
using UnityEngine.EventSystems;

namespace cavallaro.tesi.smartedifice
{
    public class HidePanel : MonoBehaviour
    {

        public GameObject panel1;
        public GameObject panel2;
        public GameObject panel3;
        public GameObject panel4;

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                if (EventSystem.current.currentSelectedGameObject.name == this.name)
                {
                    if (panel1 != null) panel1.SetActive(true);
                    if (panel2 != null) panel2.SetActive(true);
                    if (panel3 != null) panel3.SetActive(true);
                    if (panel4 != null) panel4.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
        }

        public void DisableElements()
        {
            if (panel1 != null) panel1.SetActive(false);
            if (panel2 != null) panel2.SetActive(false);
            if (panel3 != null) panel3.SetActive(false);
            if (panel4 != null) panel4.SetActive(false);
            gameObject.SetActive(true);
        }

    }
}