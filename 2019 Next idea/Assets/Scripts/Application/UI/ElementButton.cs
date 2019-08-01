﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using UnityEngine.UI;

namespace GameGUI
{
    public class ElementButton : MonoBehaviour
    {
        [SerializeField]
        //外部设置
        private string name;
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }
        /// <summary>
        /// 点击时移除对应原件，变换鼠标指针等
        /// </summary>
        private void OnClick()
        {
            if(!LevelManager.setingelement)
            {
                LevelManager.Instance().ChangeArrow(name);
                LevelManager.setingelement = true;
                LevelManager.choosingelement = name;
                ElementsPanel.RemoveElement(this.gameObject);
            }


        }

    }
}

