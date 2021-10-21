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
    public class MultiToggleBSetter
    {
        // Listeners
        public virtual UnityAction<MultiToggle.ToggleSet[], REbaseSelector> OnValueChangedListener { get; set; } = (l, s) => { };
        // Properties
        public virtual int maxEnabled { get; set; } = 0;

        public MultiToggle Set(MultiToggle c)
        {
            c.maxEnabled = maxEnabled;
            
            return c;
        }


        public MultiToggle Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<MultiToggle>());
        }

        public void SetListeners(MultiToggle c, REbaseSelector selector)
        {
            c.OnValueChanged.AddListener((l) =>
            {
                try
                {
                    OnValueChangedListener?.Invoke(l, selector);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error Executing OnValueChangedListener: " + e);
                }
            });


        }

    }
}
