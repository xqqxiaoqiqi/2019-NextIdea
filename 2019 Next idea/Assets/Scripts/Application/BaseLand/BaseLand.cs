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
        // List<Element> inputelements = new List<Element>();
        //test 
        public List<BaseLand> inputlist = new List<BaseLand>();
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
        /// 更新自身周围地格。
        /// </summary>
        public void UpdateParameter()
        {
            vector = GetComponent<Transform>().position;
            myelement = GetComponentInChildren<Element>();
            topnode = LandManager.GetLand(new Vector2(vector.x, vector.y + 1));
            leftnode = LandManager.GetLand(new Vector2(vector.x - 1, vector.y));
            rightnode = LandManager.GetLand(new Vector2(vector.x + 1, vector.y));
            bottomnode = LandManager.GetLand(new Vector2(vector.x, vector.y - 1));

        }
        /// <summary>
        /// 充能时激活上层元件并充能其非空相邻地格
        /// </summary>
        /// <param name="source">提供充能地格</param>
        public virtual bool OnCharge(BaseLand source)
        {
            
            hascharged = true;
            //如果上层元件不为空，则激活这个元件
            if (myelement != null)
                {
                    myelement.OnActive(source,this);
                    //激活上层元件,上层元件继续为周围其他地格充能。

                }
                else
                {

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
            if(node!=null)
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
        public void RequestCancelCharge(BaseLand node)
        {
            if(node!=null)
            {
                if(node.inputlist.Contains(node))
                {
                    node.inputlist.Remove(node);
                    CancelCharge();
                }

            }
        }
        /// <summary>
        /// 取消充能时调用，取消上层元件的激活状态并取消其对相邻地格的充能
        /// </summary>
        protected void CancelCharge()
        {
            if(hascharged)
            {
                //todo:取消上层元件的激活状态,并取消其对相邻地格的充能
                if(myelement!=null)
                {
                    myelement.SourceClosed(this);
                }

            }
            hascharged = false;

        }

    }
}


