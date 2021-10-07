using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace U.Reactor
{

    public class TextSetterButton : TextSetter
    {
        public override int fontSize { get; set; } = 60;
        public override TextAnchor alignment { get; set; } = TextAnchor.MiddleCenter;
    }

}
