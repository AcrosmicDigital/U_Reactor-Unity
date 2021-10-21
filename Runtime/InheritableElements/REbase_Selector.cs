using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace U.Reactor
{
    public abstract class REbaseSelector
    {
        // Track whether Dispose has been called.
        internal Func<LayoutElementBSetter> layoutElementSetter { get; set; }
        internal bool isLayoutElement { get; set; } = false;
        public bool isDisposed { get; private set; } = false;
        public ReactorId elementId { get; private set; }
        public RectTransform rectTransform { get; private set; }

        public GameObject gameObject { get; private set; }

        // Get the root
        public REcanvas.Selector root
        { get 
            {
                REbaseSelector root = this;
                int i = 0;
                while(root.parent != null && i < 200)
                {
                    root = root.parent;
                    i++;
                }

                try
                {
                    return (REcanvas.Selector)root;
                }
                catch (Exception)
                {
                    return null;
                }

                
            } 
        }

        // Get the first parent canvas
        public REcanvas.Selector parentCanvas
        {
            get
            {
                if (parent == null)
                {
                    try
                    {
                        return (REcanvas.Selector)this;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

                REbaseSelector parentCanvas = parent;

                int i = 0;
                while (parentCanvas.parent != null && parentCanvas.gameObject.GetComponent<Canvas>() == null && i < 200)
                {
                    parentCanvas = parentCanvas.parent;
                    i++;
                }

                try
                {
                    return (REcanvas.Selector)parentCanvas;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public REbaseSelector parent { get; private set; }
        public REbaseSelector[] childs { get; private set; }
        public  REbaseSelector[] brothers { get
            {
                if (parent == null)
                    return null;
                else
                    return parent.childs;
            }
        } 

        internal REbaseSelector(GameObject gameObject, ReactorId pieceId, RectTransform rectTransform)
        {
            this.gameObject = gameObject;
            this.elementId = pieceId;
            this.rectTransform = rectTransform;
        }


        internal void SetParent(REbaseSelector parent)
        {
            this.parent = parent;
        }

        internal void SetChilds(List<REbase> childs)
        {
            this.childs = childs.Select(c => c.selector).ToArray();
        }
        internal void SetChilds(List<REbaseSelector> childs)
        {
            this.childs = childs.ToArray();
        }
        internal void SetChilds(REbaseSelector[] childs)
        {
            this.childs = childs;
        }


        internal virtual void Destroy()
        {
            isDisposed = true;
            elementId = null;
            rectTransform = null;
            gameObject = null;
            parent = null;
            childs = null;
        }

    }
}
