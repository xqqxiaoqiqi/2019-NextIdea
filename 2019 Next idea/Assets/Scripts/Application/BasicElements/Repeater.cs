using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;

namespace GameTool
{
    public class Repeater : Element
    {
        private BaseLand headland;
        private BaseLand tailland;
        private float timer;
        private float delay = 1.0f;
        private void Awake()
        {
            element_ID = "repeater";
        }
        public override bool InstalizeThisElement()
        {
            base.InstalizeThisElement();
            UpdateConnect();
            return true;
        }
        public override void UpdateTexture()
        {
            GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(disable_texturepath + element_ID, typeof(Sprite));
            GetComponent<ElementParticle>().UpdateParticleType(element_ID);
            UpdateConnect();
        }
        public override void OnActive(BaseLand lastland, Element source)
        {
            StartCoroutine(ProessActive(lastland, source));
        }
        protected override void BeActive(BaseLand lastland)
        {

            myland.stepstack.Push(0);
            myland.BeforeRequestCharge(myland, headland);
            myland.stepstack.Pop();

        }
        public override void OnSilence(BaseLand lastland, Element source)
        {
            StartCoroutine(ProcessSilence(lastland, source));
        }
        protected override void BeSilence(BaseLand lastland)
        {

            myland.stepstack.Push(0);
            myland.BeforeCancelCharge(myland, headland);
            myland.stepstack.Pop();

        }
        /// <summary>
        /// 更新通信状态，旋转或初始化后调用
        /// </summary>
        private void UpdateConnect()
        {
            Vector3 vector = GetComponent<Transform>().localEulerAngles;
            if (vector.Equals(Vector3.zero))
            {

                headland = myland.leftnode;
                tailland = myland.rightnode;
            }
            else if (vector.Equals(new Vector3(0, 0, -90)) || vector.Equals(new Vector3(0, 0, 270)))
            {
                headland = myland.topnode;
                tailland = myland.bottomnode;
            }
            else if (vector.Equals(new Vector3(0, 0, -180)) || vector.Equals(new Vector3(0, 0, 180)))
            {

                headland = myland.rightnode;
                tailland = myland.leftnode;
            }
            else if (vector.Equals(new Vector3(0, 0, -270)) || vector.Equals(new Vector3(0, 0, 90)))
            {

                headland = myland.bottomnode;
                tailland = myland.topnode;
            }
            else
            {
                Debug.Log("Oh！shit");
            }
        }
        IEnumerator ProessActive(BaseLand lastland, Element source)
        {
            yield return new WaitForSeconds(delay);
            if (lastland.Equals(tailland))
            {
                if (source != null)
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
        IEnumerator ProcessSilence(BaseLand lastland, Element source)
        {
            yield return new WaitForSeconds(delay);
            if(myland.sourcelist.Count==0)
            {
                if (lastland.Equals(tailland))
                {
                    if (source != null)
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
    }

}

