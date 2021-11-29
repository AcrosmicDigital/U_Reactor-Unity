using System;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    /// <summary>
    /// Create a Slider in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class SliderBSetter<TSelector> where TSelector : REbaseSelector
    {
        // Listeners
        public virtual Action<float, TSelector> OnValueChangedListener { get; set; } = (f, s) => { };
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
        public virtual Slider.Direction direction { get; set; } = Slider.Direction.LeftToRight;
        public virtual float minValue { get; set; } = 0;
        public virtual float maxValue { get; set; } = 1;
        public virtual bool wholeNumbers { get; set; } = false;
        public virtual float value { get; set; } = 0;


        internal Slider Set(Slider c)
        {
            c.interactable = interactable;
            c.navigation = navigation;
            c.transition = transition;
            c.colors = colors;
            c.spriteState = spriteState;
            c.animationTriggers = animationTriggers;

            c.direction = direction;
            c.minValue = minValue;
            c.maxValue = maxValue;
            c.wholeNumbers = wholeNumbers;
            c.value = value;

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
