using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTool
{

    public abstract class Element : MonoBehaviour
    {
        protected bool isactive = false;
        /// <summary>
        /// 元件所在的地格引用
        /// </summary>
        public BaseLand belangland;
        public static BaseLand superland;
        //test 
        //protected List<BaseLand> landsources = new List<BaseLand>();
        /// <summary>
        /// 元件激活源
        /// </summary>
        public BaseLand landsource;
        /// <summary>
        ///元件充能的地格（输出）
        /// </summary>
        public List<BaseLand> outputlands = new List<BaseLand>();
        /// <summary>
        /// 被激活时调用，调用BeActive并充能其他相邻地格
        /// </summary>
        public virtual void OnActive( BaseLand source )
        {
            if(landsource == null)
            {
                landsource = source;
            }
            if(!isactive)
            {
                BeActive();
            }
        }
        /// <summary>
        /// 元件被激活后调用
        /// </summary>
        protected virtual void BeActive()
        {
            //充能自己所在地格的相邻地格，绝大部分的元件都是这样。
            superland = this.landsource;
            BeforeRequestCharge(belangland.topnode);
            BeforeRequestCharge(belangland.bottomnode);
            BeforeRequestCharge(belangland.leftnode);
            BeforeRequestCharge(belangland.rightnode);


        }
        public virtual void BeforeRequestCharge(BaseLand land)
        {
            if(land!=null)
            {
                if (!outputlands.Contains(land)&&!land.Equals(superland))
                {
                    if(belangland.RequestOnCharge(land, this))
                    {
                        outputlands.Add(land);
                    }
                }
            }

        }
        /// <summary>
        /// 充能源取消充能时调用,在这里判断自己是否还被激活
        /// </summary>
        /// <param name="source"></param>
        public virtual void SourceClosed(BaseLand source)
        {
            //
            if(source!=null)
            {
                if (landsource==source)
                {
                    landsource=null;

                        isactive = false;
                        CancelActive();
                }
            }
            else
            {
                isactive = false;
                CancelActive();
            }

        }
        /// <summary>
        /// 取消激活时，取消对自己相邻地格的充能
        /// </summary>
        protected virtual void CancelActive()
        {
            belangland.RequestCancelCharge(belangland.topnode, this);
            belangland.RequestCancelCharge(belangland.bottomnode, this);
            belangland.RequestCancelCharge(belangland.leftnode, this);
            belangland.RequestCancelCharge(belangland.rightnode, this);
        }
        /// <summary>
        /// 获取所在地格引用
        /// </summary>
        /// <returns></returns>
        public bool Setbelangland()
        {
            belangland = GetComponentInParent<BaseLand>();
            if(belangland!=null)
            {
                return true;
            }
            return false;
        }

    }
}