using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class MaskBSetter
    {
        public virtual bool  showMaskGraphic { get; set; } = true;

        public Mask Set(Mask c)
        {
            c.showMaskGraphic = showMaskGraphic;

            return c;
        }


        public Mask Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Mask>());
        }
    }
}
