using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTool
{
    public class NormalCharger : Element
    {
        public static List<Element> allnormalchargers = new List<Element>();
        private  void Awake()
        {
            element_ID = "normalcharger";
            allnormalchargers.Add(this);
        }
        /// <summary>
        /// 充能器被激活，如果被其他元件激活，则反相信号
        /// </summary>
        /// <param name="lastland"></param>
        public override void OnActive(BaseLand lastland,Element source)
        {
            if(lastland!=null)
            {
                myland.sourcelist.Clear();
            }
            else
            {
                if (source == null)
                {
                    processingsource.Push(this);
                }
                base.OnActive(lastland, source);
                if (processingsource.Peek().Equals(this))
                {
                    processingsource.Pop();
                }
            }
        }
        public override void OnSilence(BaseLand lastland, Element source)
        {
            if(lastland!=null)
            {
                //base.OnActive(source, land);
            }
            else
            {
                if (source == null)
                {
                    processingsource.Push(this);
                }
                base.OnSilence(lastland, source);
                if (processingsource.Peek().Equals(this))
                {
                    processingsource.Pop();
                }
            }
        }
        public override void UpdateTexture()
        {
            GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(enable_texturepath + element_ID,typeof(Sprite));
            GetComponent<ElementParticle>().UpdateParticleType(element_ID);
        }
    }
}


