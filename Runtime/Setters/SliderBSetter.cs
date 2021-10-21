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
    public class SliderBSetter
    {
        // Listeners
        public virtual UnityAction<float, REbaseSelector> OnValueChangedListener { get; set; } = (f, s) => { };
        // Properties
        // ...

        public Slider Set(Slider c)
        {


            return c;
        }


        public Slider Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Slider>());
        }


        public void SetListeners(Slider c, REbaseSelector selector)
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
