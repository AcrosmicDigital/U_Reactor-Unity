using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class ScrollbarSetter
    {
        public Scrollbar Set(Scrollbar c)
        {


            return c;
        }


        public Scrollbar Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Scrollbar>());
        }
    }
}
