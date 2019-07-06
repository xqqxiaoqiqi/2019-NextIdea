using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;

namespace GameTool
{
    public abstract class BaseLand : MonoBehaviour
    {
        public BaseLand topnode;
        public BaseLand leftnode;
        public BaseLand rightnode;
        public BaseLand bottomnode;
        protected Element topelement;
        public List<Element> sources = new List<Element>();
        //test 
        //protected List<Element> sources = new List<Element>();
        //protected bool ischarge;
        //test
        public bool hascharged;
        private Vector2 vector;
        protected void Awake()
        {
        }
        /// <summary>
        /// 获取自身周围地格。
        /// </summary>
        public void UpdateParameter()
        {
            vector = GetComponent<Transform>().position;
            topelement = GetComponentInChildren<Element>();
            topnode = LandManager.GetLand(new Vector2(vector.x, vector.y + 1));
            leftnode = LandManager.GetLand(new Vector2(vector.x - 1, vector.y));
            rightnode = LandManager.GetLand(new Vector2(vector.x + 1, vector.y));
            bottomnode = LandManager.GetLand(new Vector2(vector.x, vector.y - 1));

        }
        /// <summary>
        /// 被充能时调用，激活上层元件并充能其非空相邻地格
        /// </summary>
        /// <param name="source">提供充能地格</param>
        public virtual void OnFirstCharge(Element source)
        {
            hascharged = true;
            //if (!sources.Contains(source))
            //{
            //    sources.Add(source);
                if (topelement != null&&source!=topelement)
                {
                    topelement.OnActive(this);

                    //todo:激活上层元件,上层元件为周围其他地格充能。

                }
                else
                {

                }
            //}
          
        }
        public void RequestChargeNode(BaseLand node,Element source)
        {

            if(node!=null)
            {
                if (!node.sources.Contains(source))
                {
                    node.sources.Add(source);
                }
                if( !node.hascharged)
                {
                    node.OnFirstCharge(this.topelement);

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
                //topnode.CancelCharge();
                //leftnode.CancelCharge();
                //rightnode.CancelCharge();
                //bottomnode.CancelCharge();
            }
            hascharged = false;

        }

    }
}


