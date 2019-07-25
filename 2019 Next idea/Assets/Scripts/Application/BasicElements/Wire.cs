using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameTool
{
    public class Wire : Element
    {
        private string line_wire = "Wires/wire_0";
        private string default_wire = "Wires/wire_1";
        private string t_wire = "Wires/wire_2";
        private string l_wire = "Wires/wire_3";
        public override void UpdateTexture()
        {
            int i = 0;
            int[,] pos = new int[4,2] { {0,1},{ 0,-1},{ 1,0},{ -1,0} };
            List<Vector2> containpos = new List<Vector2>();
            for(int j = 0; j<pos.GetLength(0);j++)
            {
                if(ContainElement(transform.position,pos[j,0],pos[j,1]))
                {
                    i++;
                    containpos.Add(new Vector2(pos[j, 0], pos[j, 1]));
                }
            }
            transform.localEulerAngles = new Vector3(0, 0, 0);
            switch (i)
            {
                case 1:
                    GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(disable_texturepath + line_wire, typeof(Sprite));
                    texturename = line_wire;
                    if (containpos.First()==new Vector2(0,1)|| containpos.First() == new Vector2(0, -1))
                    {
                        transform.localEulerAngles=new Vector3(0,0,-90);
                    }
                    break;
                case 2:
                    Vector2 vect = containpos[0] + containpos[1];
                    if(vect==new Vector2(0,0))
                    {
                        GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(disable_texturepath + line_wire, typeof(Sprite));
                        texturename = line_wire;
                        Vector2 newvect = containpos[0] - containpos[1];
                        if(newvect==new Vector2(0,2)|newvect == new Vector2(0,-2))
                        {
                            transform.localEulerAngles = new Vector3(0, 0, -90);
                        }
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(disable_texturepath + l_wire, typeof(Sprite));
                        texturename = l_wire;
                        if (vect==new Vector2(1,-1))
                        {
                            //donothing
                        }
                        else if(vect==new Vector2(-1,-1))
                        {
                            transform.localEulerAngles = new Vector3(0, 0, -90);
                        }
                        else if(vect==new Vector2(-1,1))
                        {
                            transform.localEulerAngles = new Vector3(0, 0, -180);
                        }
                        else
                        {
                            transform.localEulerAngles = new Vector3(0, 0, -270);
                        }
                    }
                    break;
                case 3:
                    GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(disable_texturepath + t_wire, typeof(Sprite));
                    texturename = t_wire;
                    Vector2 vect3 = containpos[0] + containpos[1] + containpos[2];
                    if(vect3==new Vector2(0,-1))
                    {
                        //donothing
                    }
                    else if(vect3==new Vector2(-1,0))
                    {
                        transform.localEulerAngles = new Vector3(0, 0, -90);
                    }
                    else if (vect3 == new Vector2(0, 1))
                    {
                        transform.localEulerAngles = new Vector3(0, 0, -180);
                    }
                    else
                    {
                        transform.localEulerAngles = new Vector3(0, 0, -270);
                    }
                    break;
                default:
                    GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(disable_texturepath + default_wire, typeof(Sprite));
                    texturename = default_wire;
                    break;
            }
        }
    }
}


