using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    /// <summary>
    /// Create a Dropdown in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class DropdownBSetter<TSelector> where TSelector : REbaseSelector
    {
        // Listeners
        public virtual Action<int, TSelector> OnValueChangedListener { get; set; } = (v, s) => { };
        // Properties
        public virtual bool interactable { get; set; } = true;
        public virtual Selectable.Transition transition { get; set; } = Selectable.Transition.ColorTint;
        public virtual Navigation navigation { get; set; } = new Navigation
        {
            mode = Navigation.Mode.Automatic,
        };
        public virtual ColorBlock colors { get; set; } = new ColorBlock
        {
            normalColor = new Color(1, 1, 1, 1),
            highlightedColor = new Color(0.9607f, 0.9607f, 0.9607f, 1),
            pressedColor = new Color(0.7843f, 0.7843f, 0.7843f, 1),
            selectedColor = new Color(0.9607f, 0.9607f, 0.9607f, 1),
            disabledColor = new Color(0.7843f, 0.7843f, 0.7843f, 0.5019f),
            colorMultiplier = 1,
            fadeDuration = 0.1f,
        };
        public virtual SpriteState spriteState { get; set; } = new SpriteState
        {
            highlightedSprite = null,
            disabledSprite = null,
            pressedSprite = null,
            selectedSprite = null,
        };
        public virtual AnimationTriggers animationTriggers { get; set; } = new AnimationTriggers
        {
            disabledTrigger = "Disabled",
            highlightedTrigger = "Highlighted",
            normalTrigger = "Normal",
            pressedTrigger = "Pressed",
            selectedTrigger = "Selected",
        };
        public virtual int value { get; set; } = 0;
        public virtual float alphaFadeSpeed { get; set; } = 0.15f;
        public virtual List<Dropdown.OptionData> options { get; set; } = new List<Dropdown.OptionData> 
        {
            new Dropdown.OptionData
            {
                text = "Option A",
            },
            new Dropdown.OptionData
            {
                text = "Option B",
            },
            new Dropdown.OptionData
            {
                text = "Option C",
            },
        };

        internal Dropdown Set(Dropdown c)
        {
            c.interactable = interactable;
            c.navigation = navigation;
            c.transition = transition;
            c.colors = colors;
            c.spriteState = spriteState;
            c.animationTriggers = animationTriggers;


            c.alphaFadeSpeed = alphaFadeSpeed;
            c.options = options;
            c.value = value;

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
