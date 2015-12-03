using UnityEngine;
using System.Xml.Serialization;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Xml;
using System.Collections;

namespace cavallaro.tesi.smartedifice
{
    public class SceneManager : MonoBehaviour
    {
        string filename;

        private WWW GET(string url)
        {
            WWW www = new WWW(url);

            StartCoroutine(WaitForWWW(www));

            while (!www.isDone) { }

            if (www.error == null || www.error == "")
            {
                StreamWriter writer = new StreamWriter(filename);
                writer.WriteLine(www.text);
                writer.Close();
                Debug.Log("Saved XML File to directory: " + filename);
            }
            else
            {
                Debug.LogError("WWW error: " + www.error);
                return null;
            }

            return www;
        }

        IEnumerator WaitForWWW(WWW www)
        {
            yield return www;
        }

        void Awake()
        {
            string file = string.Format("{0}/{1}", Application.persistentDataPath, "settings.xml");

            importXMLSettings(file);

            filename = string.Format("{0}/{1}", Application.persistentDataPath, State.sceneFile);

            WWW www = GET(State.configPath);
            if (www == null) return;
            
            ConfigDocument document = Parser.Deserialize<ConfigDocument>(filename);
            if (document == null)
                throw new Exception(string.Format("File Config: {0} not found", filename));

            foreach(LightPrefab item in document.Lights) item.Render();

            foreach(Prefab prefab in document.Prefabs) prefab.Render();

            foreach(SmartPrefab prefab in document.SmartPrefab) prefab.Render();
        }

        private void importXMLSettings(string file)
        {
            if (string.IsNullOrEmpty(file) || !File.Exists(file))
            {
                throw new Exception(string.Format("File {0} not exist.", file));
            }

            XmlReader reader = XmlReader.Create(file);
            reader.ReadToFollowing("requestPath");
            reader.MoveToFirstAttribute();
            State.requestPath = reader.Value;

            reader.ReadToFollowing("configPath");
            reader.MoveToFirstAttribute();
            State.configPath = reader.Value;

            reader.ReadToFollowing("sceneFile");
            reader.MoveToFirstAttribute();
            State.sceneFile = reader.Value;
        }

        void Start()
        {
            InvokeRepeating("periodicUpdate", 1, 90);
        }

        void periodicUpdate()
        {
            getAll();
        }

        private void getAll()
        {
            string path = State.requestPath + "?then=get all";

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

        private void writeXMLFile()
        {
            ConfigDocument doc = new ConfigDocument();

            List<SmartPrefab> SmartPrefabs = new List<SmartPrefab>();
            List<Prefab> Prefabs = new List<Prefab>();
            List<LightPrefab> Lights = new List<LightPrefab>();

            UnityEngine.Light[] lights = GameObject.FindObjectsOfType<UnityEngine.Light>();

            foreach (UnityEngine.Light obj in lights)
            {
                LightPrefab light = new LightPrefab();
                light.Position = string.Format("{0},{1},{2}", obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);//obj.transform.position;
                light.Rotation = string.Format("{0},{1},{2}", obj.transform.rotation.eulerAngles.x, obj.transform.rotation.eulerAngles.y, obj.transform.rotation.eulerAngles.z);
                light.Name = obj.name;
                light.Intensity = obj.intensity;
                light.TypeLight = obj.type;
                light.Range = obj.range;
                Lights.Add(light);
            }

            SmartObject[] allSmartObjects = GameObject.FindObjectsOfType<SmartObject>();

            foreach (SmartObject smart in allSmartObjects)
            {
                SmartPrefab sm = new SmartPrefab();
                sm.Dimension = string.Format("{0},{1},{2}", smart.gameObject.transform.localScale.x, smart.gameObject.transform.localScale.y, smart.gameObject.transform.localScale.z);
                sm.GUID = smart.GUID;
                sm.Name = smart.gameObject.name;
                sm.Position = string.Format("{0},{1},{2}", smart.gameObject.transform.position.x, smart.gameObject.transform.position.y, smart.gameObject.transform.position.z);
                sm.Rotation = string.Format("{0},{1},{2}", smart.gameObject.transform.rotation.eulerAngles.x, smart.gameObject.transform.rotation.eulerAngles.y, smart.gameObject.transform.rotation.eulerAngles.z);
                sm.Resource = smart.gameObject.name;

                SmartPrefabs.Add(sm);
            }


            GameObject[] allObject = GameObject.FindObjectsOfType<GameObject>();

            foreach (GameObject obj in allObject)
            {
                if (obj.GetComponent<UnityEngine.Light>() == null && obj.GetComponent<SmartObject>() == null)
                {
                    Prefab p = new Prefab();
                    p.Dimension = string.Format("{0},{1},{2}", obj.gameObject.transform.localScale.x, obj.gameObject.transform.localScale.y, obj.gameObject.transform.localScale.z);
                    p.Name = obj.gameObject.name;
                    p.Position = string.Format("{0},{1},{2}", obj.gameObject.transform.position.x, obj.gameObject.transform.position.y, obj.gameObject.transform.position.z);
                    p.Rotation = string.Format("{0},{1},{2}", obj.gameObject.transform.rotation.eulerAngles.x, obj.gameObject.transform.rotation.eulerAngles.y, obj.gameObject.transform.rotation.eulerAngles.z);
                    p.Resource = obj.gameObject.name;

                    Prefabs.Add(p);
                }
            }

            doc.Prefabs = Prefabs;
            doc.Lights = Lights;
            doc.SmartPrefab = SmartPrefabs;


            string fileName = "config.xml";
            string output = string.Format("{0}/{1}", Application.persistentDataPath, fileName);
            Parser.Serialize<ConfigDocument>(doc, output);
            Debug.Log("Saved file: " + output);
        }
    }
}
