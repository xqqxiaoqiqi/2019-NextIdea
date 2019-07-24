using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using System;

namespace GameTool
{
    public abstract class BaseLand : MonoBehaviour
    {
        public BaseLand topnode;
        public BaseLand leftnode;
        public BaseLand rightnode;
        public BaseLand bottomnode;
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
        //protected bool ischarge;
        //test
        /// <summary>
        /// 充能状态
        /// </summary>
        public bool hascharged;
        public Vector2 vector;
        protected void Awake()
        {
        }
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
        /// 充能时激活上层元件并充能其非空相邻地格
        /// </summary>
        /// <param name="source">提供充能地格</param>
        public virtual bool OnCharge(BaseLand source)
        {
            //这个hascharged有啥用？？
            hascharged = true;
            //如果上层元件不为空，则激活这个元件
            if (myelement != null)
            {
                myelement.OnActive(source, this);
            }
            return true;
        }
        /// <summary>
        /// 元件尝试充能地格时调用
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sourceelement"></param>
        public bool RequestOnCharge(BaseLand node)
        {
            if (node != null)
            {

                if (!inputlist.Contains(node))
                {
                    inputlist.Add(node);
                    node.outputlist.Add(this);
                    OnCharge(node);
                    return true;
                }



            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public bool RequestCancelCharge(BaseLand node)
        {
            if (node != null)
            {
                if (inputlist.Contains(node))
                {
                    if(node.inputlist.Count==0||(node.inputlist.Count==1&&node.inputlist[0].Equals(this))||node.myelement is NormalCharger)
                    {
                        inputlist.Remove(node);
                        node.outputlist.Remove(this);
                        CancelCharge(node);
                        return true;
                    }
                    else
                    {

                    }

                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        protected bool CancelCharge(BaseLand source)
        {
            hascharged = false;
            if (myelement != null)
            {
                myelement.OnSilence(source, this);
            }
            return true;

        }

    }
}


