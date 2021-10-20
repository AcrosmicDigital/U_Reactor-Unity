using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class ToggleBSetter
    {
        public Toggle Set(Toggle c)
        {


            return c;
        }


        public Toggle Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Toggle>());
        }
    }
}
