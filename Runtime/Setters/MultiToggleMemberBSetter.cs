using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace U.Reactor
{
    public class MultiToggleMemberBSetter
    {
        // Listeners
        // ...
        // Properties
        public virtual string toggleName { get; set; } = "Toggle";
        public virtual int toggleNumber { get; set; } = 0;
        public virtual float toggleValue { get; set; } = 0f;


        internal HC.MultiToggleMember Set(HC.MultiToggleMember c)
        {
            c.toggleName = toggleName;
            c.toggleNumber = toggleNumber;
            c.toggleValue = toggleValue;

            return c;
        }


        internal HC.MultiToggleMember Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<HC.MultiToggleMember>());
        }
    }
}
