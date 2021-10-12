using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class InputFieldSetter
    {
        public InputField Set(InputField c)
        {


            return c;
        }


        public InputField Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<InputField>());
        }
    }
}
