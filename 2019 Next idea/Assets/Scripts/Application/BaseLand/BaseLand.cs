using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using System;
using GameGUI;

namespace GameTool
{
    public abstract class BaseLand : MonoBehaviour
    {
        public BaseLand topnode;
        public BaseLand leftnode;
        public BaseLand rightnode;
        public BaseLand bottomnode;
        public bool interactable;
        public Stack<int> stepstack = new Stack<int>();
        /// <summary>
        /// 地格上层元件的引用
        /// </summary>
        public Element myelement;
        /// <summary>
        /// 地格充能源(输入)
        /// </summary>
        public List<BaseLand> inputlist = new List<BaseLand>();
        /// <summary>
        /// 元件激活的地格链表，目前只在debug中用到过2019.7.23
        /// </summary>
        public List<BaseLand> outputlist = new List<BaseLand>();
        public List<Element> sourcelist = new List<Element>();
        //test
        /// <summary>
        /// 充能状态
        /// </summary>
        public bool hascharged;
        public Vector2 vector;
        /// <summary>
        /// 更新参数，因为涉及到布线问题，所以一个地格更新的事件时周围所有的地格都要更新结点
        /// </summary>
        public void UpdateLandParameter()
        {
            vector = GetComponent<Transform>().position;
            UpdateNode(this);
            UpdateNode(topnode);
            UpdateNode(bottomnode);
            UpdateNode(leftnode);
            UpdateNode(rightnode);


        }
        /// <summary>
        /// 更新传入land的结点信息
        /// </summary>
        /// <param name="land"></param>
        public void UpdateNode(BaseLand land)
        {
            if (land != null)
            {
                land.topnode = LandManager.GetLand(new Vector2(land.vector.x, land.vector.y + 1));
                land.bottomnode = LandManager.GetLand(new Vector2(land.vector.x, land.vector.y - 1));
                land.leftnode = LandManager.GetLand(new Vector2(land.vector.x - 1, land.vector.y));
                land.rightnode = LandManager.GetLand(new Vector2(land.vector.x + 1, land.vector.y));
            }

        }
        /// <summary>
        /// 判断land栈顶元素，合法则调用RequestCharge
        /// </summary>
        /// <param name="land"></param>
        public virtual void BeforeRequestCharge(BaseLand thisland, BaseLand node)
        {
            if (node != null)
            {
                if (node.stepstack.Count == 0)
                {
                    node.RequestOnCharge(thisland);
                }
            }


        }
        /// <summary>
        /// 元件尝试充能地格时调用
        /// </summary>
        /// <param name="lastland"></param>
        /// <param name="sourceelement"></param>
        public bool RequestOnCharge(BaseLand lastland)
        {
            if (!sourcelist.Contains(Element.processingsource.Peek()))
            {
                sourcelist.Add(Element.processingsource.Peek());
                OnCharge(lastland);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 充能时激活上层元件并充能其非空相邻地格
        /// </summary>
        /// <param name="lastland">提供充能地格</param>
        /// 
        public virtual bool OnCharge(BaseLand lastland)
        {
            //这个hascharged有啥用？？
            hascharged = true;
            //如果上层元件不为空，则激活这个元件
            if (myelement != null)
            {
                myelement.OnActive(lastland, Element.processingsource.Peek());
            }
            return true;
        }
        /// <summary>
        /// 判断land栈顶元素，合法则调用CanncelCharge
        /// </summary>
        /// <param name="source"></param>
        public virtual void BeforeCancelCharge(BaseLand thisland, BaseLand node)
        {
            if (node != null)
            {
                if (node.stepstack.Count == 0)
                {
                    node.RequestCancelCharge(thisland);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastland"></param>
        public bool RequestCancelCharge(BaseLand lastland)
        {
            if (lastland != null)
            {
                if (sourcelist.Contains(Element.processingsource.Peek()))
                {
                    sourcelist.Remove(Element.processingsource.Peek());
                    CancelCharge(lastland);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        protected bool CancelCharge(BaseLand lastland)
        {
            hascharged = false;
            if (myelement != null)
            {
                myelement.OnSilence(lastland, Element.processingsource.Peek());
            }
            return true;

        }
        public void OnClick()
        {

            if (!LevelViewer.isactive)
            {
                    if (LevelManager.setingelement)
                    {
                        if (myelement == null && interactable)
                        {
                            GameElementManager.Instance().AddElement(this.gameObject, LevelManager.choosingelement);
                            LevelManager.Instance().AddElementDone();
                        }
                        else
                        {
                            Debug.Log("you can not do this");
                        }
                }
                ElemenOperation.Instance().ShowOperation(myelement);


            }
            else
            {
                if (myelement.elementname.Equals("button") || myelement.name.Equals("rod"))
                {
                    myelement.GetComponent<Element>().OnActive(null, null);
                }
            }

        }
    }
}


