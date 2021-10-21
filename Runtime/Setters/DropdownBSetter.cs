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
    public class DropdownBSetter
    {
        // Listeners
        public virtual UnityAction<int, REbaseSelector> OnValueChangedListener { get; set; } = (v, s) => { };
        // Properties
        // ...


        public Dropdown Set(Dropdown c)
        {
            
            return c;
        }


        public Dropdown Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Dropdown>());
        }

        public void SetListeners(Dropdown c, REbaseSelector selector)
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
