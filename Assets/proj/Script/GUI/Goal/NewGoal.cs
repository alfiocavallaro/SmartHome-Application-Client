using UnityEngine;
using System;
using UnityEngine.UI;

namespace cavallaro.tesi.smartedifice
{
    public class NewGoal : MonoBehaviour
    {
        public InputField IF_Subject;
        public InputField IF_Room;
        public InputField IF_Sign;
        public InputField IF_Value;
        public InputField THEN_Command;
        public InputField THEN_Value;
        public InputField THEN_Target;
        public InputField THEN_Room;
        public InputField ELSE_Command;
        public InputField ELSE_Value;
        public InputField ELSE_Target;
        public InputField ELSE_Room;

        public void sendRequest()
        {
            String path = produceRequest();

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

        private String produceRequest()
        {
            string value_IF_Subject    = IF_Subject.text;
            string value_IF_Room       = IF_Room.text;
            string value_IF_Sign       = IF_Sign.text;
            string value_IF_Value      = IF_Value.text;
            string _if = "";
            if(value_IF_Subject != "") _if = value_IF_Subject;
            if (value_IF_Room != "") _if += " " + value_IF_Room;
            if (value_IF_Sign != "") _if += " " + value_IF_Sign;
            if (value_IF_Value != "") _if += " " + value_IF_Value;


            string value_THEN_Command  = THEN_Command.text;
            string value_THEN_Value    = THEN_Value.text;
            string value_THEN_Target   = THEN_Target.text;
            string value_THEN_Room     = THEN_Room.text;
            string _then = "";
            if(value_THEN_Command != "") _then = value_THEN_Command;
            if(value_THEN_Value != "") _then += " " + value_THEN_Value;
            if (value_THEN_Target != "") _then += " " + value_THEN_Target;
            if (value_THEN_Room != "") _then += " " + value_THEN_Room;


            string value_ELSE_Command  = ELSE_Command.text;
            string value_ELSE_Value    = ELSE_Value.text;
            string value_ELSE_Target   = ELSE_Target.text;
            string value_ELSE_Room     = ELSE_Room.text;
            string _else = "";
            if(value_ELSE_Command != "") _else = value_ELSE_Command;
            if (value_ELSE_Value != "") _else += " " + value_ELSE_Value;
            if (value_ELSE_Target != "") _else += " " + value_ELSE_Target;
            if (value_ELSE_Room != "") _else += " " + value_ELSE_Room;


            string path = State.requestPath;
            if(_then != "")
            {
                if(_if != "")
                {
                    if(_else != "")
                    {
                        path += "?if=" + _if + "&then=" + _then + "&else=" + _else + "&repeat=no";
                    }
                    else
                    {
                        path += "?if=" + _if + "&then=" + _then + "&repeat=no";
                    }
                }
                else
                {
                    path += "?then=" + _then;
                }
            }

            Debug.Log(path);
            return path;
        }

    }
}
