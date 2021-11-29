using System;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    /// <summary>
    /// Create a Toggle in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class ToggleBSetter<TSelector> where TSelector : REbaseSelector
    {
        // Listeners
        public virtual Action<bool, TSelector> OnValueChangedListener { get; set; } = (v, s) => { };
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
        public virtual bool isOn { get; set; } = false;
        public virtual Toggle.ToggleTransition toggleTransition { get; set; } = Toggle.ToggleTransition.Fade;


        internal Toggle Set(Toggle c)
        {
            c.interactable = interactable;
            c.navigation = navigation;
            c.transition = transition;
            c.colors = colors;
            c.spriteState = spriteState;
            c.animationTriggers = animationTriggers;

            c.isOn = isOn;
            c.toggleTransition = toggleTransition;


            return c;
        }


        internal Toggle Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Toggle>());
        }


        internal void SetListeners(Toggle c, TSelector selector)
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
