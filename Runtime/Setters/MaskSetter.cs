using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class MaskSetter
    {
        public Mask Set(Mask c)
        {


            return c;
        }


        public Mask Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Mask>());
        }
    }
}
