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
        private static bool showstory = true;
        private int nextnum = 0;
        private static Dictionary<DialogState, List<string>> dialoglist = new Dictionary<DialogState, List<string>>();
        private static string dialog_path = "LevelCanvaDataBase/DialogData/";
        private JsonData dialog_data;
        private DialogState dialogstate;
        public PanelType paneltype;
        public void InstalizeDialog(string name)
        {
            TextAsset asset  = Resources.Load<TextAsset>(dialog_path+name);
            dialog_data = JsonMapper.ToObject(asset.text);
            string[] keys = GetJsonKeys(dialog_data[0]);
            for (int i = 0; i < keys.Length; i++)
            {                
                dialoglist.Add(ChangeToEnum(keys[i]), GetValue(dialog_data[0][keys[i]][0]));
            }
        }
        private void ShowDialog(DialogState state)
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
        private void UpdateCurrConv()
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
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public void RequestDialog(DialogState state)
        {
            if (showstory|| dialoglist.ContainsKey(state))
            {
                ShowDialog(state);
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
        private DialogState ChangeToEnum(string name)
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
        private string[] GetJsonKeys(JsonData data)
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
        private List<string> GetValue(JsonData key)
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

