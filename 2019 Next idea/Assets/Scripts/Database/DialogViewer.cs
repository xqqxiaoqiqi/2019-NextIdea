using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTool;
using DataBase;
using System.Linq;
using LitJson;

namespace DataBase
{
    public enum DialogState
    {
        LoadOver,
        StopCircuit,
        Pass,
        RemoveElement,
        AddElement,
        StartCircuit,

        Null
    }
    public enum PanelType
    {
        Ready,
        Showing,
        RequestStop,
        Over,
        Null
    }
    public class DialogViewer : UnitySingleton<DialogViewer>
    {
        internal string currname;
        internal string currconv;
        internal static int currentPos;
        protected static bool showstory = true;
        protected int nextnum = 0;
        protected static Dictionary<string, List<string>> dialoglist = new Dictionary<string, List<string>>();
        protected static string dialog_path = "LevelCanvaDataBase/DialogData/";
        protected JsonData dialog_data;
        protected string dialogstate;
        public PanelType paneltype;
        public void InstalizeDialog(string name)
        {
            TextAsset asset  = Resources.Load<TextAsset>(dialog_path+name);
            dialog_data = JsonMapper.ToObject(asset.text);
            string[] keys = GetJsonKeys(dialog_data[0]);
            for (int i = 0; i < keys.Length; i++)
            {                
                dialoglist.Add(keys[i], GetValue(dialog_data[0][keys[i]][0]));
            }
        }
        protected virtual void ShowDialog(string state)
        {
                dialogstate = state;
                ShowOverProcess();
                Debug.Log("showloadover" + state);
                //todo:启用panel

        }
        internal void ShowOverProcess()
        {
            currentPos = 0;
            paneltype = PanelType.Ready;
            UpdateCurrConv();
        }
        protected void UpdateCurrConv()
        {
            if(dialoglist[dialogstate].Count>nextnum)
            {
                string content = dialoglist[dialogstate][nextnum];
                string[] contents = content.Split(':');
                currname = contents.First();
                currconv = contents.Last();
                nextnum++;
            }
            else
            {
                paneltype = PanelType.Over;
                //每个对话只会出现一次
                dialoglist.Remove(dialogstate);
                nextnum = 0;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public virtual void RequestDialog(DialogState state)
        {
            if (showstory&& dialoglist.ContainsKey(state.ToString()))
            {
                ShowDialog(state.ToString());
                paneltype = PanelType.Showing;
            }
        }
        public static void ShowPanel(GameObject thispanel)
        {
            thispanel.GetComponent<CanvasGroup>().alpha = 1;
            thispanel.GetComponent<CanvasGroup>().interactable = true;
            thispanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        public static void HidePanel(GameObject thispanel)
        {
            thispanel.GetComponent<CanvasGroup>().alpha = 0;
            thispanel.GetComponent<CanvasGroup>().interactable = false;
            thispanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        /// <summary>
        /// 字符串转换为枚举类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected DialogState ChangeToEnum(string name)
        {
            switch (name)
            {
                case "LoadOver":
                    return DialogState.LoadOver;
                case "StopCircuit":
                    return DialogState.StopCircuit;
                default:
                    return DialogState.Null;
            }
        }
        /// <summary>
        /// 获取键
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected string[] GetJsonKeys(JsonData data)
        {
            IDictionary dictionary = (IDictionary)data;
            List<string> keys = new List<string>();
            foreach (string key in dictionary.Keys)
            {
                keys.Add(key);
            }
            return keys.ToArray();
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected List<string> GetValue(JsonData key)
        {
            List<string> values = new List<string>();
            for (int i = 0; i < key.Count; i++)
            {
                values.Add(key[i].ToString());
            }
            return values;
        }
    }
}

