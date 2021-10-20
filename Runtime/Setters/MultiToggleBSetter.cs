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
        public virtual int maxEnabled { get; set; } = 0;

        public MultiToggle Set(MultiToggle c)
        {
            c.maxEnabled = maxEnabled;

            return c;
        }


        public MultiToggle Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<MultiToggle>());
        }

    }
}
