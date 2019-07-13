using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using GameTool;

public class ChargeTest : MonoBehaviour
{
  public void CircuitStartTest()
    {
        for (int i = 0; i < NormalCharger.allnormalchargers.Count;i++)
        {
            NormalCharger.allnormalchargers[i].OnActive(null,null);
        }
    }
}
