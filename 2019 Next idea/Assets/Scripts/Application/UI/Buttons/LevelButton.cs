using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using UnityEngine.UI;

namespace GameGUI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField]
        //外部设置
        private string levelnum;
        private string leveldetail;
        [SerializeField]
        private GameObject detailpanel;
        private Text nametext;
        private Text detailtext;
        private void Awake()
        {
            detailpanel = gameObject.transform.parent.gameObject.GetComponentInChildren<LevelSelectPanel>().gameObject;
            GetComponent<Button>().onClick.AddListener(OnClick);
            nametext = detailpanel. GetComponentsInChildren<Text>()[0];
            detailtext = detailpanel.GetComponentsInChildren<Text>()[1];
        }
        private void OnClick()
        {
            if(LevelSelectPanel.RequestShow(detailpanel))
            {
                DialogViewer.ShowPanel(detailpanel);
                nametext.text = levelnum;
                detailtext.text = LevelManager.Instance().RequestDetail(levelnum);
                LevelSelectPanel.SetSelectLevelNum(levelnum);
            }
        }

    }

}

