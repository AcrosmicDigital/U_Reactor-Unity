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
    public class MultiToggleBSetter<TSelector> where TSelector : REbaseSelector
    {
        // Listeners
        public virtual UnityAction<HC.MultiToggle.ToggleSet[], TSelector> OnValueChangedListener { get; set; } = (l, s) => { };
        // Properties
        public virtual int maxEnabled { get; set; } = 0;

        internal HC.MultiToggle Set(HC.MultiToggle c)
        {
            c.maxEnabled = maxEnabled;
            
            return c;
        }


        internal HC.MultiToggle Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<HC.MultiToggle>());
        }

        internal void SetListeners(HC.MultiToggle c, TSelector selector)
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
