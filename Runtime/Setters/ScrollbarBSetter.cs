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
    public class ScrollbarBSetter
    {
        // Listeners
        public virtual UnityAction<float, REbaseSelector> OnValueChangedListener { get; set; } = (v, s) => { };
        // Properties
        public virtual bool interactable { get; set; } = true;
        public virtual Selectable.Transition transition { get; set; } = Selectable.Transition.ColorTint;
        public virtual Navigation navigation { get; set; } = new Navigation 
        { 
            mode = Navigation.Mode.Automatic,  

        };
        public virtual Scrollbar.Direction direction { get; set; } = Scrollbar.Direction.LeftToRight;
        public virtual float value { get; set; } = 0;
        public virtual float size { get; set; } = .2f;
        public virtual int numberOfSteps { get; set; } = 0;

        public Scrollbar Set(Scrollbar c)
        {
            c.interactable = interactable;
            c.transition = transition;
            c.navigation = navigation;
            c.direction = direction;
            c.value = value;
            c.size = size;
            c.numberOfSteps = numberOfSteps;

            return c;
        }


        public Scrollbar Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Scrollbar>());
        }


        public void SetListeners(Scrollbar c, REbaseSelector selector)
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
