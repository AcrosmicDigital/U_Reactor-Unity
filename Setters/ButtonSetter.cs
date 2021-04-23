using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace U.Reactor
{
    public class ButtonSetter
    {
        internal REbutton.Selector selector;
        public virtual UnityAction<REbutton.Selector> OnClickListener { get; set; } = (s) => { };
        public virtual bool interactable { get; set; } = true;
        public virtual Selectable.Transition transition { get; set; } = Selectable.Transition.ColorTint;
        //public Navigation navigation = ;

        public Button Set(Button c, REbutton.Selector selector)
        {
            this.selector = selector;
            c.interactable = interactable;
            c.transition = transition;
            //c.navigation = navigation;
            c.onClick.AddListener(() => 
            {
                try
                {
                    OnClickListener?.Invoke(selector);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error Executing OnClickListener: " + e);
                }
            });

            return c;
        }

        public Button Set(GameObject gameObject, REbutton.Selector selector)
        {
            return Set(gameObject.AddComponent<Button>(), selector);
        }

    }

}
