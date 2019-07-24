using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTool;

namespace DataBase
{
    public class ChargeTest : MonoBehaviour
    {
        public GameObject test;
        public NormalCharger testcharger;
        public void Awake()
        {
            
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
            //Todo:检测是否满足通关条件。
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
        }
        public void AddElementTest()
        {
            GameElementManager.Instance().AddElement(test, "wire");
        }

        public void ChargerCancelTest()
        {
            testcharger.OnSilence(null, null);
        }
        public void ChargerOnTest()
        {
            testcharger.OnActive(null, null);
        }
    }
    }


