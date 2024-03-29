﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTool;
using LitJson;
using GameGUI;
using System;
namespace DataBase
{
    public class LevelViewer : UnitySingleton<LevelViewer>
    {
        private List<int> outputlist = new List<int>();
        public static Dictionary<string, Queue<int>> successcondition = new Dictionary<string, Queue<int>>();
        [SerializeField]
        public static Dictionary<string, Queue<int>> processingcondition = new Dictionary<string, Queue<int>>();
        public static int maxcheck = 5;
        public static  bool isactive = false;
        private static float timer = 0;
        private static float checktime = 1.0f;
        private static JsonData leveldata;
        private static string level_id;
        private string leveldata_path = "LevelCanvaDataBase/LevelData/";
        private static bool haspassed;
        internal void InstalizeLevel(string id)
        {
           TextAsset level_data = (TextAsset)Resources.Load(leveldata_path+id, typeof(TextAsset));
           leveldata = JsonMapper.ToObject(level_data.text);
           level_id = leveldata[0]["MapID"].ToString();
           LandManager.Instance().ReadMap(level_id);
            LevelManager.presentviewer.GetComponent<DialogViewer>().InstalizeDialog(level_id);
           InstalizeCondition();
           haspassed = false;
           LevelManager.presentviewer.GetComponent<DialogViewer>().RequestDialog(DialogState.LoadOver);
           for(int i=0;i<leveldata[0]["StartElement"].Count;i++)
            {
                ElementsPanel.Instance().AddElement(leveldata[0]["StartElement"][i].ToString());
            }
            CircuitStart();
            if(leveldata[0].ContainsKey("CheckTime"))
            {
                checktime = float.Parse(leveldata[0]["CheckTime"].ToString());
                Debug.Log("检测时间已重置");
            }
        }
        private void Update()
        {
            if(isactive)
            {
                timer += Time.deltaTime;
                if (timer >= checktime)
                {
                    CheckCondition();
                }

            }
        }
        /// <summary>
        /// 电路运行
        /// </summary>
        public void CircuitStart()
        {
            for (int i = 0; i < NormalCharger.allnormalchargers.Count; i++)
            {
                NormalCharger.allnormalchargers[i].OnActive(null, null);
            }
            isactive = true;
            LevelManager.presentviewer.GetComponent<DialogViewer>().RequestDialog(DialogState.StartCircuit);
            DialogViewer.ShowPanel(LevelManager.Instance().connectingpanel);
        }
        /// <summary>
        /// 电路关闭
        /// </summary>
        public void CircuitClose()
        {
            for (int i = 0; i < NormalCharger.allnormalchargers.Count; i++)
            {
                NormalCharger.allnormalchargers[i].OnSilence(null, null);
            }
            foreach(LightElement element in LightElement.lightlist)
            {
                processingcondition[element.GetLight_ID()].Clear();
            }
            isactive = false;
            LevelManager.presentviewer.GetComponent<DialogViewer>().RequestDialog(DialogState.StopCircuit);
            DialogViewer.HidePanel(LevelManager.Instance().connectingpanel);
        }
        private static void InstalizeCondition()
        {
            for(int i=0;i<LightElement.lightlist.Count;i++)
            {
                string lightelement = LightElement.lightlist[i].GetLight_ID();
                string[] conditiondata = leveldata[0]["SuccessCondition"][0][lightelement].ToString().Split(',');
                Queue<int> result=new Queue<int>();
                for(int j=0;j<conditiondata.Length;j++)
                {
                    result.Enqueue(int.Parse(conditiondata[j]));
                }
                successcondition.Add(lightelement, result);
                processingcondition.Add(lightelement, new Queue<int>());
            }

        }
        public static void UpdateCondition(string light_id,int state)
        {
            if(light_id!=null)
            {
                timer = 0;
                if (processingcondition.ContainsKey(light_id))
                {                    
                    processingcondition[light_id].Enqueue(state);
                    if(processingcondition[light_id].Count>maxcheck)
                    {
                        processingcondition[light_id].Dequeue();
                    }
                }
            }

        }
        public static void CheckCondition()
        {
            Debug.Log("checkCondition");
            for (int i=0;i<LightElement.lightlist.Count;i++)
            {
                UpdateCondition(LightElement.lightlist[i].GetLight_ID(), LightElement.lightlist[i].state);
            }
            if(ConditionCompare())
            {
                if(!haspassed)
                {
                    haspassed = true;
                    Debug.Log("Pass");
                    LevelManager.presentviewer.GetComponent<DialogViewer>().RequestDialog(DialogState.Pass);
                    LevelManager.Instance().UpdateRateList(level_id, true);
                    DialogViewer.HidePanel(LevelManager.Instance().connectingpanel);
                    DialogViewer.ShowPanel(LevelManager.Instance().successpanel);
                    //todo:通关处理
                }

            }
        }
        public  static bool ConditionCompare()
        {           
            for(int i=0;i<LightElement.lightlist.Count;i++)
            {
                string id = LightElement.lightlist[i].GetLight_ID();
                if(successcondition[id].Count==processingcondition[id].Count)
                {
                    for(int j=0;j<successcondition[id].Count;j++)
                    {
                        if(successcondition[id].ToArray()[j]!=processingcondition[id].ToArray()[j])
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public void DestroyLevel()
        {
            //back to chose;
            isactive = false;
            GameObject map = GameObject.FindGameObjectWithTag("GameMap");
            for (int i=0;i<map.transform.childCount;i++)
            {
                Destroy(map.transform.GetChild(i).gameObject);
            }
        }
    }
}


