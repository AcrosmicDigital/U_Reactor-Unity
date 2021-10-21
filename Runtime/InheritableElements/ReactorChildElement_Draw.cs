using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace U.Reactor
{
    public abstract class REchild : REbase
    {

        protected abstract Func<CanvasRendererBSetter> PropsCanvasRenderer { get; }

        #region <Components>

        protected CanvasRenderer canvasRendererCmp;

        #endregion </Components>


        protected override void CreateRoot(GameObject parent)
        {
            base.CreateRoot(parent);

            canvasRendererCmp = PropsCanvasRenderer().Set(gameObject);

        }

    }

}
