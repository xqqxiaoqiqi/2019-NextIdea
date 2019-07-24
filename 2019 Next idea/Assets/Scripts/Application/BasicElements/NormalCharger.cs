using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTool
{
    public class NormalCharger : Element
    {
        public static List<NormalCharger> allnormalchargers = new List<NormalCharger>();
        private  void Awake()
        {
            texturename = "NormalCharger";
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
                base.OnActive(lastland, source);
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
                base.OnSilence(lastland, source);
            }
        }
        public override void UpdateTexture()
        {
            GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(enable_texturepath + texturename,typeof(Sprite));
        }
    }
}


