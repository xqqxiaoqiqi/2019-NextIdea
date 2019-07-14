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
        //Debug.Log("可用地格充能源（元件）：");
        for (int i = 0; i < GetComponent<BaseLand>().inputelements.Count; i++)
        {
            if (GetComponent<BaseLand>().inputelements[i] != null)
            {
                //Debug.Log(GetComponent<BaseLand>().inputelements[i]);
            }
            else
            {
              //  Debug.Log("none");
            }

        }

        if (GetComponentInChildren<Element>() != null)
        {
            //Debug.Log("元件激活源（地格）：");
            if (GetComponentInChildren<Element>().landsource!=null)
            {
                //for(int j=0; j< GetComponentInChildren<Element>().landsources.Count; j++)
                //{
                    Debug.Log(GetComponentInChildren<Element>().landsource);

                //}

            }
            else
            {
               // Debug.Log("none");
            }
           // Debug.Log("元件激活地格（输出）：");

            if (GetComponentInChildren<Element>().outputlands != null)
            {
                for(int j=0; j< GetComponentInChildren<Element>().outputlands.Count; j++)
                {
                Debug.Log(GetComponentInChildren<Element>().outputlands[j]);

                }
            }

        }
        else
        {
           // Debug.Log("地格上没有元件");
        }
    }
}
