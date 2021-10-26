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
    public class SliderBSetter<TSelector> where TSelector : REbaseSelector
    {
        // Listeners
        public virtual UnityAction<float, TSelector> OnValueChangedListener { get; set; } = (f, s) => { };
        // Properties
        // ...

        internal Slider Set(Slider c)
        {


            return c;
        }


        internal Slider Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Slider>());
        }


        internal void SetListeners(Slider c, TSelector selector)
        {
            c.onValueChanged.AddListener((f) =>
            {
                try
                {
                    OnValueChangedListener?.Invoke(f, selector);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error Executing OnValueChangedListener: " + e);
                }
            });

        }


    }
}
