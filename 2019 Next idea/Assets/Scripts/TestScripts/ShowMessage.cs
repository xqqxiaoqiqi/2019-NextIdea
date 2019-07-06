using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTool;
using DataBase;

public class ShowMessage : MonoBehaviour
{
    public void ShowDetail()
    {
        Debug.Log("充能状态：" + GetComponent<BaseLand>().hascharged);
        GetSourcesMessage();

    }
    public void GetSourcesMessage()
    {
        Debug.Log("地格充能源（元件）：");
        for (int i = 0; i < GetComponent<BaseLand>().sources.Count; i++)
        {
            if (GetComponent<BaseLand>().sources[i] != null)
            {
                Debug.Log(GetComponent<BaseLand>().sources[i]);
            }
            else
            {
                Debug.Log("none");
            }

        }

        if (GetComponentInChildren<Element>() != null)
        {
            Debug.Log("元件激活源（地格）：");
            if (GetComponentInChildren<Element>().landsources.Count!=0)
            {
                for (int j = 0; j < GetComponentInChildren<Element>().landsources.Count; j++)
                {
                    Debug.Log(GetComponentInChildren<Element>().landsources[j]);
                }
            }
            else
            {
                Debug.Log("none");
            }

        }
        else
        {
            Debug.Log("地格上没有元件");
        }
    }
}
