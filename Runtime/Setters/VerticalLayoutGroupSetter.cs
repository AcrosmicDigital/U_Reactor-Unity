using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class VerticalLayoutGroupSetter
    {
        public RectOffset padding = new RectOffset(0,0,0,0);
        public float spacing = 0;
        public TextAnchor childAlignment = TextAnchor.UpperLeft;
        public bool reverseArrangement = false;
        public bool childControlHeight = false;
        public bool childControlWidth = false;
        public bool childScaleHeight = false;
        public bool childScaleWidth = false;
        public bool childForceExpandHeight = true;
        public bool childForceExpandWidth = true;

        public VerticalLayoutGroup Set(VerticalLayoutGroup c)
        {
            c.padding = padding;
            c.spacing = spacing;
            c.childAlignment = childAlignment;
            c.reverseArrangement = reverseArrangement;
            c.childControlHeight = childControlHeight;
            c.childControlWidth = childControlWidth;
            c.childScaleHeight = childScaleHeight;
            c.childScaleWidth = childScaleWidth;
            c.childForceExpandHeight = childForceExpandHeight;
            c.childForceExpandWidth = childForceExpandWidth;

            return c;
        }


        public VerticalLayoutGroup Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<VerticalLayoutGroup>());
        }
    }
}
