using System;
using UnityEngine;

namespace U.Reactor
{
    /// <summary>
    /// Create a HC.MultiToggle in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class MultiToggleBSetter<TSelector> where TSelector : REbaseSelector
    {
        // Listeners
        public virtual Action<HC.MultiToggle.ToggleSet[], TSelector> OnValueChangedListener { get; set; } = (l, s) => { };
        // Properties
        public virtual int maxEnabled { get; set; } = 0;

        internal HC.MultiToggle Set(HC.MultiToggle c)
        {
            c.maxEnabled = maxEnabled;
            // c.toggleDefs set in REMultiToogle
            
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
