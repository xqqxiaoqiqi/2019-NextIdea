using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTool;
using DataBase;
using UnityEngine.UI;
using LitJson;

namespace GameGUI
{
    public class ElementsPanel : UnitySingleton<ElementsPanel>
    {

        private GameObject selectelement;
        private static string buttonprefab_path = "GamePrefabs/UIPrefab/Buttons/";
        private static List<string> elementlist = new List<string>();
        public static Button cancelbutton;
        private JsonData elementdata;
        private static string elementdata_path = "";
        private void Awake()
        {
            cancelbutton = GetComponentInChildren<Button>();
            cancelbutton.onClick.AddListener(Cancel);
            cancelbutton.gameObject.SetActive(false);
        }
        private static void InstalizePanel()
        {

        }
        public static void RemoveElement(GameObject elementbutton)
        {
            GameObject.Destroy(elementbutton);
            cancelbutton.gameObject.SetActive(true);

        }
        public void AddElement(string name)
        {
            GameObject button = (GameObject)Instantiate(Resources.Load(buttonprefab_path + name, typeof(GameObject)));
            button.transform.SetParent(this.gameObject.transform);
        }
        public void Cancel()
        {
            LevelManager.Instance().elementspanel.AddElement(LevelManager.choosingelement);
            LevelManager.Instance().AddElementDone();
            cancelbutton.gameObject.SetActive(false);
        }
    }

}

