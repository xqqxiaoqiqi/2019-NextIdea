using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTool;

namespace DataBase
{
    public class ChargeTest : MonoBehaviour
    {
        public GameObject test;
        /// <summary>
        /// 电路运行
        /// </summary>
        public void CircuitStartTest()
        {
            for (int i = 0; i < NormalCharger.allnormalchargers.Count; i++)
            {
                NormalCharger.allnormalchargers[i].OnActive(null, null);
            }
        }
        /// <summary>
        /// 电路关闭
        /// </summary>
        public void CircuitCloseTest()
        {
            for (int i = 0; i < NormalCharger.allnormalchargers.Count; i++)
            {
                NormalCharger.allnormalchargers[i].OnActive(null, null);
            }
        }
        public void Addelementtest()
        {
            GameElementManager.Instance().AddElement(test, "wire");
    }
    }
}

