using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace U.Reactor
{
    public abstract class RErendererSelector : REbaseSelector
    {

        public CanvasRenderer canvasRenderer { get; private set; }  // By constructor new RErendererSelector(xxx)

        internal RErendererSelector(GameObject gameObject, HC.ReactorId pieceId, RectTransform rectTransform, CanvasRenderer canvasRenderer) : base(gameObject, pieceId, rectTransform)
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
