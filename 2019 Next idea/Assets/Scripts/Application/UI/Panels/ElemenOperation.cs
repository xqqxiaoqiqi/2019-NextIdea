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
        private static List<GameObject> buttonlist = new List<GameObject>();
        [SerializeField]
        private static GameObject thisobject;
        private void Awake()
        {
            thisobject = gameObject;
        }
        public void ShowOperation(Element element)
        {
            for(int i=0;i<buttonlist.Count;i++)
            {
                Destroy(buttonlist[i]);
            }
            buttonlist.Clear();

            if(operationelement!= null && operationelement.Equals(element))
            {
                DialogViewer.HidePanel(thisobject);
                operationelement = null;
                operationelement.myland.GetComponent<Animation>().Play("stop");
            }
            else
            {
                if(operationelement!=null)
                {
                    operationelement.myland.GetComponent<Animation>().Play("stop");

                }
                operationelement = element;
                operationelement.myland.GetComponent<Animation>().Play("flush");
                DialogViewer.ShowPanel(thisobject);
                AddButton("Details").onClick.AddListener(ShowDetail);
                if (element.myland.interactable)
                {
                    AddButton("Remove").onClick.AddListener(RequestDestroy);
                    if (element.rotateable)
                    {
                        AddButton("RotateLeft").onClick.AddListener(RequestLeftRotate);
                        AddButton("RotateRight").onClick.AddListener(RequestRightRotate);
                    }
                }
            }


        }
        public void ShowDetail()
        {
            //todo:获取文本展示panel
        }
        public void RequestLeftRotate()
        {
            operationelement.transform.Rotate(new Vector3(0, 0, -90f));
            operationelement.AfterRotate();
            //todo:重新建立通信
        }
        public void RequestRightRotate()
        {
            operationelement.transform.Rotate(new Vector3(0, 0, 90f));
            operationelement.AfterRotate();
            //todo:重新建立通信
        }
        public void RequestDestroy()
        {
            operationelement.myland.GetComponent<Animation>().Play("stop");
            GameElementManager.Instance().RemoveElement(operationelement.gameObject);
            DialogViewer.HidePanel(thisobject);
        }
        private Button AddButton(string name)
        {
            GameObject button = (GameObject)Instantiate(Resources.Load(buttonpath + name, typeof(GameObject)));
            button.transform.SetParent(thisobject.transform);
            buttonlist.Add(button);
            return button.GetComponent<Button>();
        }
    }
}

