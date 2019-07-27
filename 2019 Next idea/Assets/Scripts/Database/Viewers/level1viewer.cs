using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace DataBase
{

    public class level1viewer :DialogViewer
{
        //private Dictionary<string, int> specialstate = new Dictionary<string, int>();
        //private Dictionary<string,int> processstate = new Dictionary<string,int>();
        private void Awake()
        {
            //specialstate.Add("StartCircuit",1);
            //processstate.Add("StartCircuit",0);
        }
        public override void RequestDialog(DialogState state)
        {
            //string content = state.ToString();
            //if (specialstate.ContainsKey(content))
            //{
            //    if (processstate[content].Equals(specialstate[content]))
            //    {
            //       if(showstory)
            //        {
            //            ShowDialog(state.ToString()+ processstate[state.ToString()]);
            //            paneltype = PanelType.Showing;
            //        }
            //    }
            //    else
            //    {
            //        processstate[state.ToString()]++;
            //    }
            //}
            //else
            //{
            //    base.RequestDialog(state);
            //}
            if(state.ToString().Equals("StartCircuit"))
            {
                if(!dialoglist.ContainsKey("AddElement"))
                {
                    base.RequestDialog(state);
                }
            }
            else
            {
                base.RequestDialog(state);
            }
        }
    }
}

