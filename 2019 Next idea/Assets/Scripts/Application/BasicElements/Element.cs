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
        protected string texturename;
        protected static Dictionary<Vector3, Element> elementlist = new Dictionary<Vector3, Element>();
        /// <summary>
        /// 被激活时调用，更换材质播放特效并调用BeActive
        /// </summary>
        public virtual void OnActive( BaseLand source,BaseLand land )
        {
            //landsource是干啥的？？？？？
            if (land != null)
            {
                landsource = land;
            }

            BeActive(source);
            //材质更新，todo:特效播放
            isactive = true;
            GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(enable_texturepath + texturename, typeof(Sprite));
        }
        /// <summary>
        /// 处理激活时的标号和广播
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
            //调用结束后当前标号出栈
            myland.stepstack.Pop();

        }
        /// <summary>
        /// 判断land栈顶元素，合法则调用RequestCharge
        /// </summary>
        /// <param name="land"></param>
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
                        land.RequestOnCharge(myland);
                    }

                }
                catch
                {
                    land.RequestOnCharge(myland);
                }

            }

        }
        /// <summary>
        /// 被取消激活时调用，更换材质关掉特效并调用BeSilence
        /// </summary>
        /// <param name="source"></param>
        public virtual void OnSilence(BaseLand source,BaseLand land)
        {
            BeSilence(source);
            if(myland.inputlist.Count==0)
            {
                landsource = null;
                isactive = false;
                //材质更新。todo：取消特效播放 
                GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(disable_texturepath + texturename, typeof(Sprite));
            }
        }
        /// <summary>
        /// 处理静默时的标号和广播
        /// </summary>
        protected virtual void BeSilence(BaseLand source)
        {
            if(source==null)
            {
                myland.stepstack.Push(0);
            }
            else
            {
                int i = source.stepstack.Peek();
                myland.stepstack.Push(++i);
            }
            BeforeCancelCharge(myland.topnode);
            BeforeCancelCharge(myland.bottomnode);
            BeforeCancelCharge(myland.leftnode);
            BeforeCancelCharge(myland.rightnode);
            myland.stepstack.Pop();

        }
        /// <summary>
        /// 判断land栈顶元素，合法则调用CanncelCharge
        /// </summary>
        /// <param name="land"></param>
        public virtual void BeforeCancelCharge(BaseLand land)
        {
            if(land!=null)
            {
                try
                {
                    int i = land.stepstack.Peek();
                    if(!(i == myland.stepstack.Peek()-1|i==0))
                    {
                        land.RequestCancelCharge(myland);
                    }
                }
                catch
                {
                    land.RequestCancelCharge(myland);
                }
            }
        }
        /// <summary>
        /// 获取所在地格引用
        /// </summary>
        /// <returns></returns>
        public bool SetMyLand()
        {
            myland = GetComponentInParent<BaseLand>();

            if(myland!=null)
            {
                elementlist.Add(this.transform.position, this);
                UpdateTexture();
                UpdateNearTexture(myland.topnode);
                UpdateNearTexture(myland.bottomnode);
                UpdateNearTexture(myland.leftnode);
                UpdateNearTexture(myland.rightnode);
                return true;
            }
            return false;
        }
        public abstract void UpdateTexture();
        public void UpdateNearTexture(BaseLand land)
        {
            if (land != null)
            {
                if (land.myelement != null)
                {
                    land.myelement.UpdateTexture();
                }
            }

        }
        public static bool ContainElement(Vector3 vector,int x,int y)
        {
            if(elementlist.ContainsKey(new Vector3(vector.x+x,vector.y+y,vector.z)))
            {
                return true;
            }
            return false;
        }
    }
}