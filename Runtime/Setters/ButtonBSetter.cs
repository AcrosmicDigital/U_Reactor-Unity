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
    public class ButtonBSetter<TSelector> where TSelector : REbaseSelector
    {
        // Listeners
        public virtual UnityAction<TSelector> OnClickListener { get; set; } = (s) => { };
        // Properties
        public virtual bool interactable { get; set; } = true;
        public virtual Selectable.Transition transition { get; set; } = Selectable.Transition.ColorTint;
        public virtual NavigationSetter navigation { get; set; } = new NavigationSetter();


        internal Button Set(Button c)
        {
            c.interactable = interactable;
            c.transition = transition;
            c.navigation = navigation.Set();

            return c;
        }

        internal Button Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Button>());
        }

        internal void SetListeners(Button c, TSelector selector)
        {
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

            
        }


    }

}
