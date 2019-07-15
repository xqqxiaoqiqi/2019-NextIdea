using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTool
{
    public class Wire : Element
    {
        private string wirename;
        private Sprite wiresprite;
        public override void SetEnableTexture()
        {
            GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(enable_texturepath + "Wires/" + wirename, typeof(Sprite));
        }
    }
}


