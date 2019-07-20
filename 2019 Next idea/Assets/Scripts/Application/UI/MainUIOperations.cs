/**
 * 作用 ： 为主界面UI Button提供底层实现
 * 挂载位置 ： UI->Canvas
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIOperations : MonoBehaviour
{
    //TODO :: 后期专门建立SceneManager统一管理场景切换
    private int MainSceneID = 1;
    private int StartUpUISceneID = 0;

    [HideInInspector] public bool IsIntroPanelShowing = false;
    [SerializeField] private GameObject IntroductionPanel;

    [SerializeField] private GameObject ConfirmExitPanel;


    private void Update()
    {
        OnFocusLeaveIntroPanel();
    }


    #region Exit Button Oper
    public void OnClickExitButton()
    {
        ConfirmExitPanel.SetActive(true);
    }

    public void OnClickExitConfirmButton()
    {
        SceneManager.LoadScene(StartUpUISceneID);
    }

    public void OnClickExitCancelButton()
    {
        ConfirmExitPanel.SetActive(false);
    }
    #endregion

    #region Introduction panel Oper
    public void OnClickIntroButton()
    {
        IsIntroPanelShowing = true;

        IntroductionPanel.SetActive(true);
    }

    public void OnFocusLeaveIntroPanel()
    {

        //当点击UI以外的元素时，收起文字介绍窗口
        if (IsIntroPanelShowing && Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            IntroductionPanel.SetActive(false);

            IsIntroPanelShowing = false;
        }
        
    }
    #endregion
}
