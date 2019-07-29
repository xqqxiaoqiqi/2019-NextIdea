using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTool;
using DataBase;

namespace Test
{
    public class ShowMessage : MonoBehaviour
    {
        public void ShowDetail()
        {
            // Debug.Log("充能状态：" + GetComponent<BaseLand>().hascharged);
            GetSourcesMessage();

        }
        public void GetSourcesMessage()
        {
            if (GetComponentInChildren<Element>() != null)
            {

                Debug.Log("元件激活源：");

                if (GetComponent<BaseLand>().sourcelist != null)
                {
                    for (int j = 0; j < GetComponent<BaseLand>().sourcelist.Count; j++)
                    {
                        Debug.Log(GetComponent<BaseLand>().sourcelist[j]);

                    }
                }

            }
            else
            {
                Debug.Log("地格上没有元件");
            }
        }
    }

}
