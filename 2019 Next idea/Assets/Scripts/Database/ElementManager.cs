using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTool;

namespace DataBase
{
    public class ElementManager : UnitySingleton<ElementManager>
    {
        private static string elementprefabpath = "GamePrefabs/ElementPrefab/";

        /// <summary>
        /// 根据配置文件初始化元件
        /// </summary>
        /// <param name="land"></param>
        /// <param name="name"></param>
        internal void AddElement(GameObject land, string name)
        {
            string[] state = name.Split('_');
            GameObject element = (GameObject)Instantiate(Resources.Load(elementprefabpath + state[0], typeof(GameObject)));
            //todo:检测元件是否存在特殊状态，如有则处理
            element.transform.SetParent(land.transform);
            element.transform.position = new Vector3(land.transform.position.x, land.transform.position.y, land.transform.position.z - 0.1f);
            element.GetComponent<Element>().Setbelangland();

        }
    }
}


