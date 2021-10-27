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
    /// <summary>
    /// Create a InputField in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class InputFieldBSetter<TSelector> where TSelector : REbaseSelector
    {
        // Listeners
        public virtual UnityAction<string, TSelector> OnValueChangedListener { get; set; } = (t, s) => { };
        public virtual UnityAction<string, TSelector> OnEndEditListener { get; set; } = (t, s) => { };
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
        public virtual string text { get; set; } = "";
        public virtual int characterLimit { get; set; } = 0;
        public virtual InputField.ContentType contentType { get; set; } = InputField.ContentType.Standard;
        public virtual InputField.LineType lineType { get; set; } = InputField.LineType.SingleLine;
        public virtual InputField.InputType inputType { get; set; } = InputField.InputType.Standard;
        public virtual TouchScreenKeyboardType keyboardType { get; set; } = TouchScreenKeyboardType.Default;
        public virtual InputField.CharacterValidation characterValidation { get; set; } = InputField.CharacterValidation.None;
        public virtual float caretBlinkRate { get; set; } = 0.85f;
        public virtual int caretWidth { get; set; } = 1;
        public virtual bool customCaretColor { get; set; } = false;
        public virtual Color selectionColor { get; set; } = new Color(0.6588f, 0.8078f, 1, 0.7529f);
        public virtual bool shouldHideMobileInput { get; set; } = false;
        public virtual bool readOnly { get; set; } = false;
        public virtual bool shouldActivateOnSelect { get; set; } = true;
        public virtual Color caretColor { get; set; } = new Color(0.1960f, 0.1960f, 0.1960f, 1);

        internal InputField Set(InputField c)
        {
            c.interactable = interactable;
            c.navigation = navigation;
            c.transition = transition;
            c.colors = colors;
            c.spriteState = spriteState;
            c.animationTriggers = animationTriggers;
            c.text = text;
            c.characterLimit = characterLimit;
            c.contentType = contentType;
            c.lineType = lineType;
            c.inputType = inputType;
            c.keyboardType = keyboardType;
            c.characterValidation = characterValidation;
            c.caretBlinkRate = caretBlinkRate;
            c.caretWidth = caretWidth;
            c.customCaretColor = customCaretColor;
            c.caretColor = caretColor;
            c.selectionColor = selectionColor;
            c.shouldHideMobileInput = shouldHideMobileInput;
            c.readOnly = readOnly;
            c.shouldActivateOnSelect = shouldActivateOnSelect;

            return c;
        }


        internal InputField Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<InputField>());
        }

        internal void SetListeners(InputField c, TSelector selector)
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
