using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace DataBase
{

    public class level1viewer :DialogViewer
{
        public override void RequestDialog(DialogState state)
        {
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

