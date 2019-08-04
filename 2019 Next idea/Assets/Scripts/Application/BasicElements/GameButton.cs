using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameGUI;
using DataBase;

namespace GameTool
{
    public class GameButton : Element
    {
        private void Awake()
        {
            element_ID = "button";
            NormalCharger.allnormalchargers.Add(this);
        }
        public override void UpdateTexture()
        {
            GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(disable_texturepath + element_ID, typeof(Sprite));
            GetComponent<ElementParticle>().UpdateParticleType(element_ID);
        }
        public override void OnActive(BaseLand lastland, Element source)
        {
            StartCoroutine(ButtonOnActive(lastland, source));
        }
        public override void OnSilence(BaseLand lastland, Element source)
        {
            if(isactive)
            {
                if (lastland != null)
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

        }
        IEnumerator ButtonOnActive(BaseLand lastland, Element source)
        {
            if (lastland != null)
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
            yield return new WaitForSeconds(0.5f);
            OnSilence(null, null);
        }
    }
}

