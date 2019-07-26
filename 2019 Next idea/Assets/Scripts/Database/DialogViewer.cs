using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTool;
using DataBase;
using System;
using LitJson;

namespace DataBase
{
    public enum DialogState
    {
        LoadOver,
        StopCircuit,
        Null
    }
    public class DialogViewer : UnitySingleton<DialogViewer>
    {
        private static bool showstory = true;
        private static Dictionary<DialogState, List<string>> dialoglist = new Dictionary<DialogState, List<string>>();
        private static string dialog_path = "LevelCanvaDataBase/DialogData/";
        private JsonData dialog_data;
        public void InstalizeDialog(string name)
        {
            TextAsset asset = (TextAsset)Resources.Load(dialog_path + name, typeof(TextAsset));
            dialog_data = JsonMapper.ToObject(asset.text);
            string[] keys = GetJsonKeys(dialog_data[0]);
            for (int i = 0; i < keys.Length; i++)
            {                
                dialoglist.Add(ChangeToEnum(keys[i]), GetValue(dialog_data[0][keys[i]][0]));
            }
        }
        private void ShowDialog(DialogState state)
        {
            Debug.Log("showloadover" + state);
            //todo:调用播放开关
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public void RequestDialog(DialogState state)
        {
            if (showstory)
            {
                ShowDialog(state);
            }
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

