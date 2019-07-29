using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

namespace DataBase
{
    public class LevelManager : UnitySingleton<LevelManager>
    {
        internal static Dictionary<string, bool> ratelist = new Dictionary<string, bool>();
        private static Dictionary<string, string> detaillist = new Dictionary<string, string>();
        public GameObject[] levelviewers;
        [SerializeField]
        public static GameObject resentviewer;
        public GameObject levelselectpanel;
        public GameObject gamepanel;
        private string detailpath = "LevelCanvaDataBase/LevelDescription/LevelDescription";
        private JsonData detaildata;
        [SerializeField]
        public List<string> levelnums = new List<string>();
        private void Awake()
        {
            TextAsset asset = Resources.Load<TextAsset>(detailpath);
            detaildata = JsonMapper.ToObject(asset.text);
            for(int i=0;i<detaildata.Count;i++)
            {
                detaillist.Add(detaildata[i]["LevelID"].ToString(), detaildata[i]["Description"].ToString());
            }
            for(int i=0;i<levelnums.Count;i++)
            {
                ratelist.Add(levelnums[i], false);
            }
        }
        private int GetCount(string num)
        {
            for (int i = 0; i < levelnums.Count; i++)
            {
               if(levelnums[i].Equals(num))
                {
                    return i;
                }
            }
            return 10000;
        }
        internal void StartLevel(string name)
        {
            int i = GetCount(name);
            UpdateDialogViewer(i);
            LevelViewer.Instance().InstalizeLevel(name);
            DialogViewer.HidePanel(levelselectpanel);
            DialogViewer.ShowPanel(gamepanel);
        }
        internal void UpdateDialogViewer(int i)
        {
            if(resentviewer!=null)
            {
                resentviewer.SetActive(false);
            }
            resentviewer = levelviewers[i];
            resentviewer.SetActive(true);

        }
        internal void UpdateRateList(string key,bool value)
        {
            if(ratelist.ContainsKey(key))
            {
                ratelist[key] = value;
            }
        }
        public string RequestDetail(string num)
        {
            if(detaillist.ContainsKey(num))
            {
             return detaillist[num];
            }
            return "敬请期待";
        }
    }

}

