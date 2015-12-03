using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace cavallaro.tesi.smartedifice
{
    class GUIGoal : MonoBehaviour
    {
        public Rect windowRect = new Rect(20, 20, 120, 50);

        void OnGUI()
        {
            windowRect = GUI.Window(0, windowRect, DoMyWindow, "Goal");
        }

        void DoMyWindow(int windowID)
        {
            if (GUI.Button(new Rect(10, 20, 100, 20), "Nuova Query"))
            {
                GameObject canvas = GameObject.Find("MyGoalPanel");

                canvas.SetActiveRecursively(true);
                foreach(HidePanel pan in GameObject.FindObjectsOfType<HidePanel>())
                {
                    pan.DisableElements();
                }
                
                GameObject FPSController = GameObject.Find("FPSController");

                FPSController.GetComponent<FirstPersonController>().enabled = false;

                SmartObject.setAllNotPickable();
            }
        }

        public void enableFPSController()
        {
            GameObject FPSController = GameObject.Find("FPSController");

            FPSController.GetComponent<FirstPersonController>().enabled = true;

            SmartObject.setAllPickable();
        }

    }
}
