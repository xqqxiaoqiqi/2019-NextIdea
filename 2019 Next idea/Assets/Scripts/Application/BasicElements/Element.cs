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
        public BaseLand myland;
        /// <summary>
        /// 元件激活源
        /// </summary>
        public BaseLand landsource;
        protected static string enable_texturepath = "Texture/ElementsTexture/Enable/";
        protected static string disable_texturepath = "Texture/ElementsTexture/Disable/";
        /// <summary>
        /// 被激活时调用，调用BeActive并充能其他相邻地格
        /// </summary>
        public virtual void OnActive( BaseLand source,BaseLand land )
        {
            if(land != null)
            {
                landsource = land;
            }
                BeActive(source);
            SetEnableTexture();
        }
        /// <summary>
        /// 元件被激活后调用
        /// </summary>
        protected virtual void BeActive(BaseLand source)
        {
            //如果传入为空，说明是电源元件，标0入栈
            if(source==null)
            {
                myland.stepstack.Push(0);
            }
            //若不为空，取栈顶元素加一入栈
            else
            {
                int i = source.stepstack.Peek();
                myland.stepstack.Push(++i);
            }
            //充能自己所在地格的相邻地格。
            BeforeRequestCharge(myland.topnode);
            BeforeRequestCharge(myland.bottomnode);
            BeforeRequestCharge(myland.leftnode);
            BeforeRequestCharge(myland.rightnode);
            myland.stepstack.Pop();
            //调用结束后当前标号出栈

        }
        public virtual void BeforeRequestCharge(BaseLand land)
        {
            if(land!=null)
            {
                //如果下一个结点的栈顶刚好比这个结点的栈顶小1/等于0，说明信号回流/回到起始始点，什么都不做。
                try
                {
                    int i = land.stepstack.Peek();
                    if (!(i == myland.stepstack.Peek() - 1 | i == 0))
                    {
                        if (land.RequestOnCharge(myland))
                        {

                        }
                    }

                }
                catch
                {
                    land.RequestOnCharge(myland);

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
            myland.RequestCancelCharge(myland.topnode);
            myland.RequestCancelCharge(myland.bottomnode);
            myland.RequestCancelCharge(myland.leftnode);
            myland.RequestCancelCharge(myland.rightnode);
        }
        /// <summary>
        /// 获取所在地格引用
        /// </summary>
        /// <returns></returns>
        public bool Setbelangland()
        {
            myland = GetComponentInParent<BaseLand>();

            if(myland!=null)
            {
                return true;
            }
            return false;
        }
        public abstract void SetEnableTexture();
    }
}