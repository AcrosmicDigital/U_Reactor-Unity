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
    public class NavigationSetter
    {
        public virtual Navigation.Mode mode { get; set; } = Navigation.Mode.Automatic;

        internal Navigation Set()
        {
            var c = new Navigation();

            c.mode = mode;

            return c;
        }


    }
}
