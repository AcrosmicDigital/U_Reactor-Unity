using System;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    /// <summary>
    /// Create a Button in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class ButtonBSetter<TSelector> where TSelector : REbaseSelector
    {
        // Listeners
        public virtual Action<TSelector> OnClickListener { get; set; } = (s) => { };
        // Properties
        public virtual bool interactable { get; set; } = true;
        public virtual Selectable.Transition transition { get; set; } = Selectable.Transition.ColorTint;
        public virtual Navigation navigation { get; set; } = new Navigation 
        {
            mode = Navigation.Mode.Automatic,
        };
        public virtual ColorBlock colors { get; set; } = new ColorBlock
        {
            normalColor = new Color(1,1,1,1),
            highlightedColor = new Color(0.9607f, 0.9607f, 0.9607f,1),
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

        internal Button Set(Button c)
        {
            c.interactable = interactable;
            c.navigation = navigation;
            c.transition = transition;
            c.colors = colors;
            c.spriteState = spriteState;
            c.animationTriggers = animationTriggers;

            return c;
        }

        internal Button Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Button>());
        }

        internal void SetListeners(Button c, TSelector selector)
        {
            c.onClick.AddListener(() =>
            {
                try
                {
                    OnClickListener?.Invoke(selector);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error Executing OnClickListener: " + e);
                }
            });

            
        }


    }

}
