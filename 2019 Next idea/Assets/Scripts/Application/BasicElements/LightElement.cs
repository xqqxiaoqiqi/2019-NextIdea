using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;

namespace GameTool
{
    public class LightElement : Element
    {
        public static List<LightElement> lightlist = new List<LightElement>();
        public int state;
        private void Awake()
        {
            element_ID = "light";
            lightlist.Add(this);
        }
        public override void UpdateTexture()
        {
            GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(enable_texturepath + element_ID, typeof(Sprite));
            GetComponent<ElementParticle>().UpdateParticleType(element_ID);
        }
        public override void OnActive(BaseLand lastland, Element source)
        {
            base.OnActive(lastland, source);
            state = 1;
            LevelViewer.UpdateCondition(this, state);
        }
        public override void OnSilence(BaseLand lastland, Element source)
        {
            base.OnSilence(lastland, source);
            state = 0;
            LevelViewer.UpdateCondition(this, state);
        }
    }
}

