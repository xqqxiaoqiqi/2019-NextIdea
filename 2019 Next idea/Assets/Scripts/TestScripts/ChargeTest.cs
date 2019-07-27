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
            LevelViewer.Instance().InstalizeLevel("0_1");
        }
        /// <summary>
        /// 电路运行
        /// </summary>
        public void CircuitStartTest()
        {
            LevelViewer.Instance().CircuitStart();
            //Todo:检测是否满足通关条件。
        }
        /// <summary>
        /// 电路关闭
        /// </summary>
        public void CircuitCloseTest()
        {
            LevelViewer.Instance().CircuitClose();
        }
        public void AddElementTest()
        {
            GameElementManager.Instance().RequestAddElement(test, "wire");
        }

        public void ChargerCancelTest()
        {
            testcharger.OnSilence(null, null);
        }
        public void ChargerOnTest()
        {
            testcharger.OnActive(null, null);
        }
        public void RemoveElementTest()
        {
            GameElementManager.Instance().RemoveElement(test);
        }
    }
    }


