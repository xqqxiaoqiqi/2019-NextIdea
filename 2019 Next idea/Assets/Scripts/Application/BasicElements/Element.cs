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
        public static Element processingsource;
        protected static string enable_texturepath = "Texture/ElementsTexture/Enable/";
        protected static string disable_texturepath = "Texture/ElementsTexture/Disable/";
        protected string element_ID;
        protected static Dictionary<Vector3, Element> elementlist = new Dictionary<Vector3, Element>();
        protected bool rotateable;
        /// <summary>
        /// 被激活时调用，更换材质播放特效并调用BeActive
        /// </summary>
        public virtual void OnActive( BaseLand lastland,Element source )
        {
            //landsource是干啥的？？？？？
            if (source == null)
            {
                processingsource = this;
            }

            BeActive(lastland);
            isactive = true;
            GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(enable_texturepath + element_ID, typeof(Sprite));
            GetComponent<ElementParticle>().PlayParticle();
        }
        /// <summary>
        /// 处理激活时的标号和广播
        /// </summary>
        protected virtual void BeActive(BaseLand lastland)
        {
            //如果传入为空，说明是电源元件，标0入栈
            if(lastland==null)
            {
                myland.stepstack.Push(0);
            }
            //若不为空，取栈顶元素加一入栈
            else
            {
                int i = lastland.stepstack.Peek();
                myland.stepstack.Push(++i);
            }
            //充能自己所在地格的相邻地格。
            myland.BeforeRequestCharge(myland, myland.topnode);
            myland.BeforeRequestCharge(myland, myland.bottomnode);
            myland.BeforeRequestCharge(myland, myland.leftnode);
            myland.BeforeRequestCharge(myland, myland.rightnode);


            //调用结束后当前标号出栈
            myland.stepstack.Pop();

        }

        /// <summary>
        /// 被取消激活时调用，更换材质关掉特效并调用BeSilence
        /// </summary>
        /// <param name="lastland"></param>
        public virtual void OnSilence(BaseLand lastland,Element source)
        {
            //landsource是干啥的？？？？？
            if (source == null)
            {
                processingsource = this;
            }

            BeSilence(lastland);
            //材质更新，todo:特效播放
            if(lastland==null||myland.sourcelist.Count==0)
            {
                isactive = false;
                GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(disable_texturepath + element_ID, typeof(Sprite));
                GetComponent<ElementParticle>().StopParticle();
            }
        }
        /// <summary>
        /// 处理静默时的标号和广播
        /// </summary>
        protected virtual void BeSilence(BaseLand lastland)
        {
            //如果传入为空，说明是电源元件，标0入栈
            if (lastland == null)
            {
                myland.stepstack.Push(0);
            }
            //若不为空，取栈顶元素加一入栈
            else
            {
                int i = lastland.stepstack.Peek();
                myland.stepstack.Push(++i);
            }
            //充能自己所在地格的相邻地格。
            myland.BeforeCancelCharge(myland, myland.topnode);
            myland.BeforeCancelCharge(myland, myland.bottomnode);
            myland.BeforeCancelCharge(myland, myland.leftnode);
            myland.BeforeCancelCharge(myland, myland.rightnode);
            //调用结束后当前标号出栈
            myland.stepstack.Pop();

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
        public void RequestDestroy()
        {
            elementlist.Remove(this.transform.position);
            UpdateTexture();
            UpdateNearTexture(myland.topnode);
            UpdateNearTexture(myland.bottomnode);
            UpdateNearTexture(myland.leftnode);
            UpdateNearTexture(myland.rightnode);
            GameObject.Destroy(this.gameObject);
        }
    }
}