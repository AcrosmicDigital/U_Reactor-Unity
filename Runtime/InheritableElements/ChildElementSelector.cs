using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace U.Reactor
{
    public abstract class ChildElementSelector : ElementSelector
    {

        public CanvasRenderer canvasRenderer { get; private set; }

        internal ChildElementSelector(GameObject gameObject, ReactorId pieceId, RectTransform rectTransform, CanvasRenderer canvasRenderer) : base(gameObject, pieceId, rectTransform)
        {
            this.canvasRenderer = canvasRenderer;
        }


        internal override void Destroy()
        {
            base.Destroy();
            canvasRenderer = null;
        }

    }

}
