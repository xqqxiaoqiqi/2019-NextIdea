using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataBase
{
    public class LevelManager : UnitySingleton<LevelManager>
    {
        internal static Dictionary<string, bool> ratelist = new Dictionary<string, bool>();
        public GameObject[] levels;
        public GameObject resentviewer;
        private void Awake()
        {

            ratelist.Add("0_1", false);
        }
        private void Start()
        {
            StartLevel(0, "0_1");
        }
        internal void StartLevel(int i,string name)
        {
            UpdateDialogViewer(i);
            LevelViewer.Instance().InstalizeLevel(name);

        }
        internal void UpdateDialogViewer(int i)
        {
            if(resentviewer!=null)
            {
                resentviewer.SetActive(false);
                resentviewer = levels[i];
                resentviewer.SetActive(true);
            }
        }
        internal void UpdateRateList(string key,bool value)
        {
            if(ratelist.ContainsKey(key))
            {
                ratelist[key] = value;
            }
        }

    }

}

