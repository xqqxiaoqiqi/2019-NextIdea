using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using GameTool;
using UnityEngine.UI;

namespace GameGUI
{
    public class ElemenOperation : UnitySingleton<ElemenOperation>
    {
        [SerializeField]
        private static Element operationelement;
        private Button leftrotate;
        private Button rightrotate;
        private Button delete;
        private Button details;
        private static string buttonpath = "GamePrefabs/UIPrefab/Buttons/";
        [SerializeField]
        private static GameObject thisobject;
        private void Awake()
        {
            thisobject = gameObject;
        }
        public void ShowOperation(Element element)
        {
            operationelement = element;
            DialogViewer.ShowPanel(thisobject);
            if(element.rotateable)
            {
                GameObject leftrotate = (GameObject)Instantiate(Resources.Load(buttonpath + "RotateLeft", typeof(GameObject)));
                leftrotate.transform.SetParent(thisobject.transform);
                leftrotate.GetComponent<Button>().onClick.AddListener(RequestLeftRotate);
                GameObject rightrotate = (GameObject)Instantiate(Resources.Load(buttonpath + "RotateRight", typeof(GameObject)));
                rightrotate.transform.SetParent(thisobject.transform);
                rightrotate.GetComponent<Button>().onClick.AddListener(RequestRightRotate);
            }
        }
        public void ShowDetail()
        {
            //todo:获取文本展示panel
        }
        public void RequestLeftRotate()
        {
            operationelement.transform.Rotate(new Vector3(0, 0, -90f));
            //todo:重新建立通信
        }
        public void RequestRightRotate()
        {
            operationelement.transform.Rotate(new Vector3(0, 0, 90f));
            //todo:重新建立通信
        }
        public void RequestDestroy()
        {
            GameElementManager.Instance().RemoveElement(operationelement.gameObject);
            DialogViewer.HidePanel(thisobject);
        }
    }
}

