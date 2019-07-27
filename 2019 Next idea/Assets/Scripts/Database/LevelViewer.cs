using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTool;
using LitJson;
using System;
namespace DataBase
{
    public class LevelViewer : UnitySingleton<LevelViewer>
    {
        private List<int> outputlist = new List<int>();
        public static Dictionary<string, Queue<int>> successcondition = new Dictionary<string, Queue<int>>();
        public static Dictionary<string, Queue<int>> processingcondition = new Dictionary<string, Queue<int>>();
        public static int maxcheck = 5;
        private bool isactive = false;
        private static float timer = 0;
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
           DialogViewer.Instance().InstalizeDialog(level_id);
           InstalizeCondition();
           haspassed = false;
           CircuitStart();
           DialogViewer.Instance().RequestDialog(DialogState.LoadOver);
        }
        private void Update()
        {
            if(isactive)
            {
                timer += Time.deltaTime;
                if (timer >= 3.0f)
                {
                    CheckCondition();;
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
            DialogViewer.Instance().RequestDialog(DialogState.StartCircuit);

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
            DialogViewer.Instance().RequestDialog(DialogState.StopCircuit);
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
                    DialogViewer.Instance().RequestDialog(DialogState.Pass);
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
    }
}


