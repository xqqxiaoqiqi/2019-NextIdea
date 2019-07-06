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
            allnormalchargers.Add(this);
            isactive = true;
        }
        public override void OnActive(BaseLand source)
        {
            if(source!=null)
            {
                isactive = false;
                //todo:反相信号。
            }
            else
            {
                    BeActive();
            }
        }

    }
}


