using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTool
{
    public class NormalCharger : Element
    {
        public static List<NormalCharger> allnormalchargers = new List<NormalCharger>();
        private string elementname = "NormalCharger";
        private  void Awake()
        {
            allnormalchargers.Add(this);
            isactive = true;
        }
        /// <summary>
        /// 充能器被激活，如果被其他元件激活，则反相信号
        /// </summary>
        /// <param name="source"></param>
        public override void OnActive(BaseLand source)
        {
            if(source!=null)
            {
                isactive = false;
                //todo:更换材质，反相信号。
            }
            else
            {
                    BeActive();
            }
        }

        public override void SetEnableTexture()
        {
            GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(enable_texturepath + elementname,typeof(Sprite));
        }
    }
}


