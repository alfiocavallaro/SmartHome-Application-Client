using UnityEngine;
using System;
using UnityStandardAssets.Characters.FirstPerson;

namespace cavallaro.tesi.smartedifice
{
    public class SmartObject : MonoBehaviour
    {
        public string GUID;
        protected float settedValue;
        protected bool isOn = false;

        protected Texture2D cursorTexture = null;

        private bool windowVisible = false;
        private string stringToEdit = "";

        public bool isPickable { get; set; }

        void OnGUI()
        {
            if (windowVisible)
            {
                GUI.Window(1, new Rect(Screen.width - Screen.width / 2, Screen.height - Screen.height / 2, 175, 100), DoMyWindow, "Set Value");
            }
        }

        void DoMyWindow(int windowID)
        {
            stringToEdit = GUI.TextField(new Rect(65, 20, 50, 20), stringToEdit, 5);

            if (GUI.Button(new Rect(10, 45, 75, 20), "Cancel"))
            {
                cancelPopup();
            }

            if (GUI.Button(new Rect(90, 45, 75, 20), "OK"))
            {
                cancelPopup();
                if(stringToEdit != null && stringToEdit != "") request(stringToEdit);       
            }

            if (GUI.Button(new Rect(10, 75, 75, 20), "ON"))
            {
                cancelPopup();
                request("ON");
            }

            if (GUI.Button(new Rect(90, 75, 75, 20), "OFF"))
            {
                cancelPopup();
                request("OFF");
            }
        }

        private void cancelPopup()
        {
            windowVisible = false;
            GameObject FPSController = GameObject.Find("FPSController");
            FPSController.GetComponent<FirstPersonController>().enabled = true;

            setAllPickable();
        }

        private void request(String val)
        {
            string path = getRequestPath(val);
            if (path != null)
            {
                try
                {
                    string resp = Request.newRequest(path, "POST");
                    AnalyzeResponse.analyze(resp);
                }
                catch (Exception e)
                {
                    PopupMessage pop = GameObject.FindObjectOfType<PopupMessage>();
                    if (pop != null)
                    {
                        pop.showMessage(e.Message);
                        return;
                    }
                }
            }
        }

        public void Awake()
        {
            cursorTexture = Resources.Load("hand-pointer") as Texture2D;
            isPickable = true;
        }

        public void OnMouseOver()
        {
            if(cursorTexture != null && isPickable)
            {
                Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
            }            
        }

        public void OnMouseExit()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }


        void OnMouseDown()
        {
            if (isPickable)
            {
                setAllNotPickable();

                GameObject FPSController = GameObject.Find("FPSController");
                FPSController.GetComponent<FirstPersonController>().enabled = false;
                windowVisible = true;
            }
        }

        protected virtual String getRequestPath(String value)
        {
            String _command = "set";
            String _target = this.GUID;
            String _value = value;

            String _if = "null";
            String _else = "null";
            String _then = _command + " " + _value + " " + _target;

            String path = State.requestPath + "?if=" + _if + "&then=" + _then + "&else=" + _else;
            return path;
        }


        protected virtual void ON()
        {

        }

        protected virtual void OFF()
        {

        }

        public static SmartObject findByGUID(String guidString)
        {

            if (!isGuid(guidString)) return null;

            SmartObject[] smarts = FindObjectsOfType<SmartObject>();

            foreach(SmartObject obj in smarts)
            {
                if (guidString == obj.GUID) return obj;
            }

            return null;
        }

        private static bool isGuid(String guidString)
        {
            if (guidString == null) throw new ArgumentNullException("guidString");
            try
            {
                new Guid(guidString);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }

        }

        public void produceServerResponse(ServerResponse serverResponse)
        {
            if(serverResponse != null)
            {
                String Switch = serverResponse.Switch;
                if(Switch != null)
                {
                    if (Switch == "on") isOn = true;
                    if (Switch == "off") isOn = false;
                }
                
                String SettedValue = serverResponse.settedValue;
                if(SettedValue != null)
                {
                    float.TryParse(SettedValue, out settedValue);
                }

                if (isOn)  ON();
                else OFF();

                PopupMessage pop = GameObject.FindObjectOfType<PopupMessage>();
                if(pop != null)
                {
                    String onOff = (isOn) ? "ON" : "OFF";
                    String msg = this.name + " setted " + onOff;
                    if (SettedValue != null)
                    {
                        if (SettedValue != "-") msg += " value: " + settedValue;
                    } 
                    
                    pop.showMessage(msg);
                }
            }
        }

        public static void setAllPickable()
        {
            SmartObject[] smarts = FindObjectsOfType<SmartObject>();

            foreach (SmartObject obj in smarts)
            {
                obj.isPickable = true;
            }
        }

        public static void setAllNotPickable()
        {
            SmartObject[] smarts = FindObjectsOfType<SmartObject>();

            foreach (SmartObject obj in smarts)
            {
                obj.isPickable = false;
            }
        }
    }
}
