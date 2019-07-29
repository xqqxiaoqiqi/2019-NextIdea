/**
 * 作用 ： 统一管理当前元件库中的元件。同时控制着界面UI的展示
 * 挂载位置 ： GameManager 
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ElementInventory : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> ElementList;

    [SerializeField]
    private Transform ElementUIParent;

    [SerializeField]
    private List<GameObject> ElementUIPrefebList;

    [SerializeField]
    private EleInvUIManager UIManager;

    private void Start()
    {

        ElementList = new List<GameObject>();
        
    }

    /// <summary>
    /// 向元件库中添加新的元件，同时，在UI上生成一个对应图标
    /// TODO：：添加新的脚本，专门控制元件库的UI操作
    /// </summary>
    /// <param name="ElementType"></param>
    /// <returns></returns>
    public bool AddElement(string ElementType)
    {
        foreach (var GO in ElementUIPrefebList)
        {

            if ( GO.transform.CompareTag(ElementType) )
            {
                GameObject ele = Instantiate(GO, ElementUIParent) as GameObject;

                ele.GetComponent<ElementUIController>().ElementsParent = ElementUIParent;
                ele.GetComponent<ElementUIController>().inventory = this;
                

                UIManager.AddElement(ele);
                ElementList.Add(ele);

                return true;
            }
        }

        return false;
    }


    public bool RemoveElement(GameObject GO)
    {

        if (UIManager.RemoveElement(GO) && ElementList.Remove(GO))
        {
            Debug.Log("Remove");

            return true;
        }

        return false;
    }
}


/*
 
     using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementInventory : MonoBehaviour
{

    [HideInInspector] public List<Transform> ElementList;

    [SerializeField] private Transform ElementUIParent;

    [SerializeField] private List<GameObject> ElementUIPrefebList;

    [SerializeField] private List<RectTransform> ElementSlotList;

    private void Awake()
    {
        ElementSlotList = new List<RectTransform>();

        //Add all the pos in HeartP
        ElementSlotList.AddRange(ElementUIParent.Find("ElementSlots").GetComponentsInChildren<RectTransform>());
        ElementSlotList.Remove(ElementUIParent.Find("ElementSlots").GetComponent<RectTransform>());

    }

    public bool AddElement(string ElementType)
    {
        GameObject heart = null;

        foreach (var GO in ElementUIPrefebList)
        {
            if (GO.name == ElementType)
            {
                heart = Instantiate(GO, this.ElementUIParent) as GameObject;

            }
        }

        if (heart == null) return false;

        ElementList.Add(heart.GetComponent<Transform>());

        heart.GetComponent<RectTransform>().position = ElementSlotList[ElementList.Count - 1].position;

        return true;
    }

    public bool RemoveHeart(RectTransform trans)
    {

        //remove the hearttrans from the list
        if (!HeartTrans.Remove(trans))
        {
            return false;
        }

        //destroy the used heart
        Destroy(trans.gameObject);

        //resort the heart icons
        for (int i = 0; i < HeartTrans.Count; i++)
        {
            HeartTrans[i].position = HeartSlots[i].position;
        }

        //if (HeartTrans.Count <= 0)
        //{
        //    //player die!
        //    WorldInfo.GetPlayerController().OnPlayerDeath();

        //}

        return true;
    }



}



     
     */
