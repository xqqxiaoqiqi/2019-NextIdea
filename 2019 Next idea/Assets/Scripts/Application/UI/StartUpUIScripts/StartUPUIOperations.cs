using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUPUIOperations : MonoBehaviour
{
    [SerializeField]
    private GameObject SettingPanel;

    [SerializeField]
    private GameObject IntroductionPanel;
    private bool IsIntroPanelShowing;

    private void Update()
    {

        //TODO：：目前在Update中每时每刻的检测是否应关闭文字介绍窗口。 个人建议还是在文字介绍界面创建一个Exit按钮。
        if (IsIntroPanelShowing && Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            OnFocusLeaveIntroPanel();
        }
    }


    public void OnClickExitButton()
    {
        Application.Quit();
    }

    public void OnClickSettingButton()
    {
        SettingPanel.SetActive(true);
    }

    public void OnClickSettingExitPanel()
    {
        SettingPanel.SetActive(false);
    }

    public void OnClickIntroButton()
    {
        IntroductionPanel.SetActive(true);
    }

    public void OnFocusLeaveIntroPanel()
    {
        IntroductionPanel.SetActive(false);
    }

}
