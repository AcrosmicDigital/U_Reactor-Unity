using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class LayoutElementBSetter
    {
        // Listeners
        // ...
        // Properties
        public virtual bool ignoreLayout { get; set; } = false;
        public virtual float minWidth { get; set; } = -1;
        public virtual float minHeight { get; set; } = -1;
        public virtual float preferredWidth { get; set; } = -1;
        public virtual float preferredHeight { get; set; } = -1;
        public virtual float flexibleWidth { get; set; } = -1;
        public virtual float flexibleHeight { get; set; } = -1;
        public virtual int layoutPriority { get; set; } = 1;

        public LayoutElement Set(LayoutElement c)
        {
            c.ignoreLayout = ignoreLayout;
            c.minWidth = minWidth;
            c.minHeight = minHeight;
            c.preferredWidth = preferredWidth;
            c.preferredHeight = preferredHeight;
            c.flexibleWidth = flexibleWidth;
            c.flexibleHeight = flexibleHeight;
            c.layoutPriority = layoutPriority;

            return c;
        }


        public LayoutElement Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<LayoutElement>());
        }
    }
}
