using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTool;
using DataBase;

public class ShowMessage : MonoBehaviour
{
    public void ShowDetail()
    {
       // Debug.Log("充能状态：" + GetComponent<BaseLand>().hascharged);
        GetSourcesMessage();

    }
    public void GetSourcesMessage()
    {

        Debug.Log("inputlist：");
        for (int i = 0; i < GetComponent<BaseLand>().inputlist.Count; i++)
        {
            if (GetComponent<BaseLand>().inputlist[i] != null)
            {

                Debug.Log(GetComponent<BaseLand>().inputlist[i]);
            }
            else
            {
              //  Debug.Log("none");
            }

        }

        if (GetComponentInChildren<Element>() != null)
        {
            Debug.Log("元件激活源（地格）：");
            if (GetComponentInChildren<Element>().landsource!=null)
            {
                    Debug.Log(GetComponentInChildren<Element>().landsource);
            }
            else
            {
               // Debug.Log("none");
            }
            Debug.Log("元件激活地格（输出）：");

            if (GetComponent<BaseLand>().outputlist != null)
            {
                for(int j=0; j< GetComponent<BaseLand>().outputlist.Count; j++)
                {
                Debug.Log(GetComponent<BaseLand>().outputlist[j]);

                }
            }

        }
        else
        {
           // Debug.Log("地格上没有元件");
        }
    }
}
