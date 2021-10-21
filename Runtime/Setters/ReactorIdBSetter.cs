using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace U.Reactor
{
    public class ReactorIdBSetter
    {
        // Listeners
        // ...
        // Properties
        public virtual string id { get; set; } = "";
        public virtual string[] className { get; set; } = new string[0];


        internal HC.ReactorId Set(Type elementType, HC.ReactorId c)
        {
            return c.Set(elementType, id, className);
        }

        internal HC.ReactorId Set(Type elementType, GameObject gameObject)
        {
            return Set(elementType, gameObject.AddComponent<HC.ReactorId>());
        }

    }

}
