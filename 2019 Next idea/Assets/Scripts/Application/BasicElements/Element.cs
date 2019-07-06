using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTool
{

    public abstract class Element : MonoBehaviour
    {
        protected bool isactive = false;
        protected BaseLand belangland;
        //test 
        //protected List<BaseLand> landsources = new List<BaseLand>();
        public List<BaseLand> landsources = new List<BaseLand>();
        /// <summary>
        /// 被激活时调用，调用BeActive并充能其他相邻地格
        /// </summary>
        public virtual void OnActive( BaseLand source)
        {
            if(!landsources.Contains(source))
            {
                landsources.Add(source);
            }
            if(!isactive)
            {
                BeActive();
            }
        }
        /// <summary>
        /// 充能自己和相邻地格
        /// </summary>
        protected virtual void BeActive()
        {
            //belangland.topnode.OnCharge(this);
            //belangland.bottomnode.OnCharge(this);
            //belangland.leftnode.OnCharge(this);
            //belangland.rightnode.OnCharge(this);
            belangland.OnFirstCharge(this);
            belangland.RequestChargeNode(belangland.topnode,this);
            belangland.RequestChargeNode(belangland.bottomnode,this);
            belangland.RequestChargeNode(belangland.leftnode,this);
            belangland.RequestChargeNode(belangland.rightnode,this);


        }
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