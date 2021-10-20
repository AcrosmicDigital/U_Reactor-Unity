using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace U.Reactor
{
    public class CanvasRendererBSetter
    {

        public virtual bool cullTransparentMesh { get; set; } = true;

        public CanvasRenderer Set(CanvasRenderer c)
        {
            c.cullTransparentMesh = cullTransparentMesh;

            return c;
        }

        public CanvasRenderer Set(GameObject gameObject)
        {
            return Set (gameObject.AddComponent<CanvasRenderer>());
        }
    }

}
