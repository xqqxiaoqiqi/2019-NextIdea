using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTool;

namespace DataBase
{
    public class GameElementManager : UnitySingleton<GameElementManager>
    {
        private static string elementprefabpath = "GamePrefabs/ElementPrefab/";

        /// <summary>
        /// 初始化指定元件到传入gameobject的子节点。
        /// 测试的时候name赋值“wire”就可以。
        /// </summary>
        /// <param name="land"></param>
        /// <param name="name"></param>
        public void AddElement(GameObject land, string name)
        {
            string[] state = name.Split('_');
            GameObject element = (GameObject)Instantiate(Resources.Load(elementprefabpath + state[0], typeof(GameObject)));
            //todo:检测元件是否存在特殊状态，如有则处理
            switch(state[0])
            {
                case "light":
                    if(state.Length>1)
                    {
                        element.GetComponent<LightElement>().SetLight_ID(name);
                    }
                    break;
            }
            element.transform.SetParent(land.transform);
            element.transform.localPosition = new Vector3(0,0, - 0.1f);
            land.GetComponent<BaseLand>().myelement = land.GetComponentInChildren<Element>();
            element.GetComponent<Element>().SetMyLand();

        }
        public void RemoveElement(GameObject element)
        {
            element.GetComponent<Element>().RequestDestroy();
        }
    }
}


