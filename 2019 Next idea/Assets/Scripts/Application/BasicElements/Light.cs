using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTool
{
    public class Light : Element
    {
        private void Awake()
        {
            element_ID = "light";
        }
        public override void UpdateTexture()
        {
            GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(enable_texturepath + element_ID, typeof(Sprite));
            GetComponent<ElementParticle>().UpdateParticleType(element_ID);
        }

    }
}

