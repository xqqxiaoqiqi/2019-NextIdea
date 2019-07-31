using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using DataBase;
using UnityEngine.UI;


namespace GameGUI
{
    public class LevelSelectPanel : MonoBehaviour
    {
        [SerializeField]
        private static string selectlevelnum;
        [SerializeField]
        private static GameObject showingpanel;
        private Button enterbutton;
        private Button cancelbutton;
        private void Awake()
        {
            enterbutton = GetComponentsInChildren<Button>()[0];
            cancelbutton = GetComponentsInChildren<Button>()[1];
            enterbutton.onClick.AddListener(EnterButtonOnClick);
            cancelbutton.onClick.AddListener(CancelButtonOnClick);
        }
        internal static void SetSelectLevelNum(string num)
        {
            selectlevelnum = num;
        }
        internal static bool RequestShow(GameObject panel)
        {
            if(showingpanel==null)
            {
                showingpanel = panel;
                return true;
            }
            else if(showingpanel.Equals(panel))
            {
                return false;
            }
            else
            {
                DialogViewer.HidePanel(showingpanel);
                showingpanel = panel;
                return true;
            }
        }
        private void EnterButtonOnClick()
        {
            LevelManager.Instance().StartLevel(selectlevelnum);
        }
        private void CancelButtonOnClick()
        {
            DialogViewer.HidePanel(showingpanel);
            showingpanel = null;
        }
    }
}

