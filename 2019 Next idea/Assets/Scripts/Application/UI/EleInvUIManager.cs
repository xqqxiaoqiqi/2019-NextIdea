﻿/**
 * 作用 ：由ElementInventory传入所需生成元件的信息，控制其在UI上的位置
 * 挂载位置 ： Canvas->Inventory
 * */

using System;
using System.Collections.Generic;
using UnityEngine;

public class EleInvUIManager : MonoBehaviour
{
    [SerializeField]
    private List<RectTransform> ElementList;

    [SerializeField]
    private List<RectTransform> ElementSlotList;

    [SerializeField]
    private ElementController controller;

    private void Awake()
    {
        ElementList = new List<RectTransform>();

        ElementSlotList = new List<RectTransform>();

        ElementSlotList.AddRange(transform.Find("ElementSlots").GetComponentsInChildren<RectTransform>());
        ElementSlotList.Remove(transform.Find("ElementSlots").GetComponent<RectTransform>());

        //Debug.Log(ElementSlotList.Count);
    }

    public bool AddElement(GameObject ele)
    {
        //Debug.Log(ElementCounts + "   " + ElementSlotList.Count);
        //Debug.Log(ElementSlotList[0].position + "    " + ele.GetComponent<RectTransform>().position);

        ele.GetComponent<RectTransform>().position = ElementSlotList[ElementList.Count].position;

        ElementList.Add(ele.GetComponent<RectTransform>());

        return true;
    }

    public bool RemoveElement(GameObject ele)
    {
        if (ElementList.Remove(ele.GetComponent<RectTransform>()))
        {
            Destroy(ele);

            //TODO:: 可以添加重新排序函数，每次从元件库拿取元件时，重排/整理显示元件
            for(int i = 0; i < ElementList.Count; i++)
            {
                ElementList[i].position = ElementSlotList[i].position;
            }

            return true;
        }

        return false;
    }

    public void OnClickLeftRotateButton()
    {
        float rotateAngles = -90;
        controller.GetSelectedElement().Rotate(new Vector3(0, 0, rotateAngles));
    }

    public void OnClickRightRotateButton()
    {
        float rotateAngles = 90;
        controller.GetSelectedElement().Rotate(new Vector3(0, 0, rotateAngles));
    }

    public void OnClickRemoveButton()
    {
        controller.RemoveSelectedElement();
    }

    public void OnClickMoveButton()
    {
        controller.CanDrag = true;
        Camera.main.GetComponent<CameraOperation>().CanCameraMoving = false;
    }

}