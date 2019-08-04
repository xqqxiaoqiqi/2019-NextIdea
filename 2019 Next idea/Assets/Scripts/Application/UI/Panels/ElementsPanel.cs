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
        private static List<GameObject> elementlist = new List<GameObject>();
        public static Button cancelbutton;
        private JsonData elementdata;
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
            elementlist.Remove(elementbutton);

        }
        public void AddElement(string name)
        {
            GameObject button = (GameObject)Instantiate(Resources.Load(buttonprefab_path + name, typeof(GameObject)));
            button.transform.SetParent(this.gameObject.transform);
            elementlist.Add(button);
        }
        public void Cancel()
        {
            LevelManager.Instance().elementspanel.AddElement(LevelManager.choosingelement);
            LevelManager.Instance().AddElementDone();
            cancelbutton.gameObject.SetActive(false);
        }
        public void DestroyAllElements()
        {
            for (int i = 0; i < elementlist.Count; i++)
            {
                Destroy(elementlist[i]) ;
            }
            elementlist.Clear();
        }
    }

}

