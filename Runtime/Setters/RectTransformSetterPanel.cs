using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace U.Reactor
{

    public class RectTransformSetterPanel : RectTransformBaseSetter
    {
        public override float width { get; set; } = 0;
        public override float height { get; set; } = 0;
        public override Vector2 anchorMin { get; set; } = Vector2.zero;
        public override Vector2 anchorMax { get; set; } = Vector2.one;
    }

}
