using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;

namespace GameTool
{
    public class LightElement : Element
    {
        public static List<LightElement> lightlist = new List<LightElement>();
        public int state=0;
        private string light_id;
        private void Awake()
        {
            element_ID = "light";
            lightlist.Add(this);
        }
        public void SetLight_ID(string name)
        {
            light_id = name;
        }
        public string GetLight_ID()
        {
            if(light_id!=null)
            {
                return light_id;

            }
            return null;
        }
        public override void UpdateTexture()
        {
            GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(disable_texturepath + element_ID, typeof(Sprite));
            GetComponent<ElementParticle>().UpdateParticleType(element_ID);
        }
        public override void OnActive(BaseLand lastland, Element source)
        {
            base.OnActive(lastland, source);
            state = 1;
            // LevelViewer.UpdateCondition(light_id, state);
            LevelViewer.CheckCondition();
        }
        public override void OnSilence(BaseLand lastland, Element source)
        {
            base.OnSilence(lastland, source);
            state = 0;
            //LevelViewer.UpdateCondition(light_id, state);
            LevelViewer.CheckCondition();
        }
    }
}

