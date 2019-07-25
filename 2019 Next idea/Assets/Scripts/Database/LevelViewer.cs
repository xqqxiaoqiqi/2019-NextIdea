using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTool;

namespace DataBase
{
    public class LevelViewer : UnitySingleton<LevelViewer>
    {
        private List<int> outputlist = new List<int>();
        public static Dictionary<LightElement, List<int>> successcondition = new Dictionary<LightElement, List<int>>();
        public static Dictionary<LightElement, List<int>> processingcondition = new Dictionary<LightElement, List<int>>();
        private bool isactive = false;
        private float timer = 0;
        private void InstalizeLevel()
        {

        }
        private void Update()
        {
            if(isactive)
            {
                timer += Time.time;
                if (timer >= 1.0f)
                {
                    CheckCondition();
                    timer = 0;
                }

            }
        }
        /// <summary>
        /// 电路运行
        /// </summary>
        public void CircuitStartTest()
        {
            for (int i = 0; i < NormalCharger.allnormalchargers.Count; i++)
            {
                NormalCharger.allnormalchargers[i].OnActive(null, null);
            }
            isactive = true;           
        }
        /// <summary>
        /// 电路关闭
        /// </summary>
        public void CircuitCloseTest()
        {
            for (int i = 0; i < NormalCharger.allnormalchargers.Count; i++)
            {
                NormalCharger.allnormalchargers[i].OnSilence(null, null);
            }
            isactive = false;
        }
        private static void InstalizeCondition()
        {

        }
        public static void UpdateCondition(LightElement light,int state)
        {
            if (processingcondition.ContainsKey(light))
            {
                processingcondition[light].Add(state);
            }
        }
        private void CheckCondition()
        {
            for(int i=0;i<LightElement.lightlist.Count;i++)
            {
                UpdateCondition(LightElement.lightlist[i], LightElement.lightlist[i].state);
            }
            if(processingcondition.Equals(successcondition))
            {
                Debug.Log("Pass");
                //todo:通关处理
            }
        }
    }
}


