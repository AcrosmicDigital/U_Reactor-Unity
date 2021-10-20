using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class MultiToggleBSetter
    {

        public MultiToggle Set(MultiToggle c)
        {
            c.maxEnabled = 0;
            c.setNameAsText = true;

            return c;
        }


        public MultiToggle Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<MultiToggle>());
        }

    }
}
