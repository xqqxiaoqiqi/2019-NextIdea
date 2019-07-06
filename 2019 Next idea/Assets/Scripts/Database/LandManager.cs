﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTool;

namespace DataBase
{
    public class LandManager : UnitySingleton<LandManager>
    {
        private static Dictionary<Vector2, BaseLand> landmap = new Dictionary<Vector2, BaseLand>();
        private TextAsset mapdata;
        private string datapath = "LevelCanvaDatabase/";
        private string landprefabpath = "GamePrefabs/GroundPrefab/";
        private void Awake()
        {
            ReadMap("landtestdata");
        }
        private void ReadMap(string mapname)
        {
            mapdata = Resources.Load(datapath + mapname, typeof(TextAsset)) as TextAsset;
            if(mapdata!=null)
            {
                System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;
                string[] lines = mapdata.text.Split(new char[] { '\r', '\n' }, option);
                for (int i=0; i<lines.Length; i++)
                {
                    string[] elementdata = lines[i].Split(',');
                    for(int j=0; j<elementdata.Length; j++)
                    {
                        if(!elementdata[j].Equals("null"))
                        {
                            string[] datadetail = elementdata[j].Split('|');
                            GameObject land = (GameObject)Instantiate(Resources.Load(landprefabpath + datadetail[0], typeof(GameObject)));
                            land.transform.position = new Vector2(j, lines.Length-1-i);
                            landmap.Add(land.transform.position, land.GetComponent<BaseLand>());
                            try
                            {
                                if (datadetail[1] != null)
                                {
                                    ElementManager.Instance().AddElement(land, datadetail[1]);
                                }
                            }
                            catch
                            {
                                //donothinghere
                            }
                        }
                    }
                }
            }
            SetBorderNode();
        }
        private void SetBorderNode()
        {
            foreach (BaseLand land in landmap.Values)
            {
                land.UpdateParameter();
            }
        }
        public static BaseLand GetLand(Vector2 vector)
        {
            if(landmap.ContainsKey(vector))
            {
                return landmap[vector];
            }
            else
            {
                return null;
            }
        }


        private void Start()
        {
            Debug.Log("done");
        }
    }
}

