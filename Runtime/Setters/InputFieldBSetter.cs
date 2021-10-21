﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace U.Reactor
{
    public class InputFieldBSetter
    {
        // Listeners
        public virtual UnityAction<string, REbaseSelector> OnValueChangedListener { get; set; } = (t, s) => { };
        public virtual UnityAction<string, REbaseSelector> OnEndEditListener { get; set; } = (t, s) => { };
        // Properties
        // ...

        public InputField Set(InputField c)
        {
            

            return c;
        }


        public InputField Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<InputField>());
        }

        public void SetListeners(InputField c, REbaseSelector selector)
        {
            c.onValueChanged.AddListener((t) =>
            {
                try
                {
                    OnValueChangedListener?.Invoke(t, selector);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error Executing OnValueChangedListener: " + e);
                }
            });

            c.onEndEdit.AddListener((t) =>
            {
                try
                {
                    OnEndEditListener?.Invoke(t, selector);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error Executing OnEndEditListener: " + e);
                }
            });

        }
    }
}
