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
    public class ToggleBSetter
    {
        // Listeners
        public virtual UnityAction<float, REbaseSelector> OnValueChangedListener { get; set; } = (v, s) => { };
        // Properties
        // ...

        public Toggle Set(Toggle c)
        {


            return c;
        }


        public Toggle Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Toggle>());
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
