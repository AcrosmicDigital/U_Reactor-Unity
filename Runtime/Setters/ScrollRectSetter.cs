using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class ScrollRectSetter
    {
        public ScrollRect Set(ScrollRect c)
        {


            return c;
        }


        public ScrollRect Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<ScrollRect>());
        }
    }
}
