using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace U.Reactor
{
    public abstract class RErenderer : REbase
    {
        protected abstract Func<CanvasRendererBSetter> PropsCanvasRenderer { get; }


        #region Components

        protected CanvasRenderer canvasRendererCmp;

        #endregion Components


        #region Drawers

        protected override void CreateRoot(GameObject parent)
        {
            base.CreateRoot(parent);

            canvasRendererCmp = PropsCanvasRenderer().Set(gameObject);

        }

        #endregion Drawers


    }

}
