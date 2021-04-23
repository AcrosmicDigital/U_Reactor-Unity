using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace U.Reactor
{
    public class ElementIdSetter
    {

        public virtual string id { get; set; } = "";
        public virtual string[] className { get; set; } = new string[0];


        internal ElementId Set(ElementId c)
        {
            return c.Set(id, className);
        }

        internal ElementId Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<ElementId>());
        }

    }

}
