using UnityEngine;
using System.Collections;
using System;
using Procurios.Public;

namespace cavallaro.tesi.smartedifice
{
    public class AnalyzeResponse
    {

        private static ArrayList parseJSON(String resp)
        {
            ArrayList response = new ArrayList();

            ArrayList list = (ArrayList)JSON.JsonDecode(resp);

            if (list == null)
            {
                response.Add(resp);
                return response;
            }

            foreach (Hashtable ob in list)
            {
                ServerResponse r = new ServerResponse();

                if (ob.ContainsKey("guid"))
                {
                    r.GUID = ob["guid"].ToString();
                }

                if (ob.ContainsKey("switch"))
                {
                    r.Switch = ob["switch"].ToString();
                }

                if (ob.ContainsKey("measuredVal"))
                {
                    r.measuredValue = ob["measuredVal"].ToString();
                }

                if (ob.ContainsKey("settedVal"))
                {
                    r.settedValue = ob["settedVal"].ToString();
                }

                response.Add(r);
            }

            return response;
        }

        public static void analyze(String resp)
        {
            ArrayList response = parseJSON(resp);

            try
            {
                foreach (String s in response)
                {
                    PopupMessage pop = GameObject.FindObjectOfType<PopupMessage>();
                    if (pop != null)
                    {
                        pop.showMessage(s);
                        return;
                    }

                }

            }
            catch (InvalidCastException) { }
            

            foreach(ServerResponse s in response)
            {
                String guid = s.GUID;

                SmartObject obj = SmartObject.findByGUID(guid);

                if(obj != null)
                {
                    obj.produceServerResponse(s);
                }
            }
        }

    }
}
