using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class DropdownBSetter
    {
        public Dropdown Set(Dropdown c)
        {


            return c;
        }


        public Dropdown Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Dropdown>());
        }
    }
}
