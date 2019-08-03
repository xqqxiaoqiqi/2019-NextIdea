using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using GameTool;

namespace GameGUI
{
    public class LevelPanel : MonoBehaviour
    {
        /// <summary>
        /// 运行按钮
        /// </summary>
        public void RunOnClick()
        {
            if (LevelViewer.isactive)
            {
                LevelViewer.Instance().CircuitClose();
            }
            else
            {
                LevelViewer.Instance().CircuitStart();

            }
        }
        /// <summary>
        /// 信息
        /// </summary>
        public void MessageOnClick()
        {

        }
        /// <summary>
        /// 选项
        /// </summary>
        public void OptionsOnClick()
        {

        }
        /// <summary>
        /// 返回
        /// </summary>
        public void BackOnClick()
        {
            DialogViewer.ShowPanel(LevelManager.Instance().exitpanel);
        }
        public void EnterOnClick()
        {
            DialogViewer.HidePanel(LevelManager.Instance().successpanel);
            DialogViewer.HidePanel(LevelManager.Instance().exitpanel);
            LevelManager.Instance().CloseLevel();
            DialogViewer.HidePanel(LevelManager.Instance().gamepanel);
            DialogViewer.ShowPanel(LevelManager.Instance().levelselectpanel);
        }
        public void CancelOnClick()
        {
            DialogViewer.HidePanel(LevelManager.Instance().exitpanel);
        }
    }

}
