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
        /// <param name="source"></param>
        public override void OnActive(BaseLand source,BaseLand land)
        {
            if(source!=null)
            {
                //base.OnSilence(source, land);
            }
            else
            {
                base.OnActive(source, land);
            }
        }
        public override void OnSilence(BaseLand source, BaseLand land)
        {
            if(source!=null)
            {
                //base.OnActive(source, land);
            }
            else
            {
                BeSilence(source);
                landsource = null;
                isactive = false;
                //材质更新。todo：取消特效播放 
                GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(disable_texturepath + texturename, typeof(Sprite));
            }
        }
        public override void UpdateTexture()
        {
            GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(enable_texturepath + texturename,typeof(Sprite));
        }
    }
}


