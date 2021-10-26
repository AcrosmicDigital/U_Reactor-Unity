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
    public class DropdownBSetter<TSelector> where TSelector : REbaseSelector
    {
        // Listeners
        public virtual UnityAction<int, TSelector> OnValueChangedListener { get; set; } = (v, s) => { };
        // Properties
        public virtual bool interactable { get; set; } = true;
        public virtual Selectable.Transition transition { get; set; } = Selectable.Transition.ColorTint;
        public virtual NavigationSetter navigation { get; set; } = new NavigationSetter();
        public virtual int value { get; set; } = 0;
        public virtual float alphaFadeSpeed { get; set; } = 0.15f;

        internal Dropdown Set(Dropdown c)
        {
            c.interactable = interactable;
            c.transition = transition;
            c.navigation = navigation.Set();
            c.value = value;
            c.alphaFadeSpeed = alphaFadeSpeed;

            return c;
        }


        internal Dropdown Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Dropdown>());
        }

        internal void SetListeners(Dropdown c, TSelector selector)
        {
            c.onValueChanged.AddListener((v) =>
            {
                try
                {
                    OnValueChangedListener?.Invoke(v, selector);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error Executing OnValueChangedListener: " + e);
                }
            });


        }

    }
}
