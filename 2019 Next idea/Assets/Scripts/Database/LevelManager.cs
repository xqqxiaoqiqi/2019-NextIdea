using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using GameTool;
using GameGUI;

namespace DataBase
{
    public class LevelManager : UnitySingleton<LevelManager>
    {
        internal static Dictionary<string, bool> ratelist = new Dictionary<string, bool>();
        private static Dictionary<string, string> detaillist = new Dictionary<string, string>();
        public static bool setingelement=false;
        public static string choosingelement;
        [SerializeField]
        private Texture2D arrow;
        public GameObject[] levelviewers;
        [SerializeField]
        public static GameObject resentviewer;
        public GameObject levelselectpanel;
        public GameObject gamepanel;
        public ElementsPanel elementspanel;
        private string detailpath = "LevelCanvaDataBase/LevelDescription/LevelDescription";
        private JsonData detaildata;
        [SerializeField]
        private static Element selectingelementname;
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
        private void OnGUI()
        {
            
        }
        /// <summary>
        /// 获取关卡名在链表中的位置
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 开始关卡处理
        /// </summary>
        /// <param name="name"></param>
        internal void StartLevel(string name)
        {
            int i = GetCount(name);
            UpdateDialogViewer(i);
            LevelViewer.Instance().InstalizeLevel(name);
            DialogViewer.HidePanel(levelselectpanel);
            DialogViewer.ShowPanel(gamepanel);
        }
        /// <summary>
        /// 加载对话子类
        /// </summary>
        /// <param name="i"></param>
        internal void UpdateDialogViewer(int i)
        {
            if(resentviewer!=null)
            {
                resentviewer.SetActive(false);
            }
            resentviewer = levelviewers[i];
            resentviewer.SetActive(true);

        }
        /// <summary>
        /// 更新某一关卡通关情况
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        internal void UpdateRateList(string key,bool value)
        {
            if(ratelist.ContainsKey(key))
            {
                ratelist[key] = value;
            }
        }
        /// <summary>
        /// 获取关卡详情介绍文本
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string RequestDetail(string num)
        {
            if(detaillist.ContainsKey(num))
            {
             return detaillist[num];
            }
            return "敬请期待";
        }
        public void AddElementDone()
        {
            ChangeArrow();
            choosingelement = null;
            setingelement = false;            
        }
        /// <summary>
        /// 更换鼠标指针材质
        /// </summary>
        public static void ChangeArrow()
        {
            //todo:实现
        }
    }

}

