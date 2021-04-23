using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace U.Reactor
{
    // Add URelementId, RectTransform, CanvasRenderer
    public abstract class ReactorChildElement : ReactorElement
    { 


        #region <Components>

        protected CanvasRenderer canvasRendererCmp;

        #endregion </Components>


        #region <Setters>

        // Gameobject
        public Func<CanvasRendererSetter> propsCanvasRenderer = () => new CanvasRendererSetter();

        #endregion </Setters>


        protected override void CreateRoot(GameObject parent)
        {
            base.CreateRoot(parent);

            canvasRendererCmp = propsCanvasRenderer().Set(gameObject);

        }

    }

}
