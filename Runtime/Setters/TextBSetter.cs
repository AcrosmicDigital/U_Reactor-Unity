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
    public class TextBSetter<TSelector> where TSelector : REbaseSelector
    {
        // Listeners
        public virtual UnityAction<bool, TSelector> OnCullStateChangedListener { get; set; } = (b, s) => { };
        // Properties
        public virtual string text { get; set; } = "Text";
        public virtual Font font { get; set; } = Resources.GetBuiltinResource<Font>("Arial.ttf");
        public virtual FontStyle fontStyle { get; set; } = FontStyle.Normal;
        public virtual int fontSize { get; set; } = 70;
        public virtual float lineSpacing { get; set; } = 1;
        public virtual bool richText { get; set; } = true;
        public virtual TextAnchor alignment { get; set; } = TextAnchor.UpperLeft;
        public virtual bool alignByGeometry { get; set; } = false;
        public virtual HorizontalWrapMode horizontalOverflow { get; set; } = HorizontalWrapMode.Overflow;
        public virtual VerticalWrapMode verticalOverflow { get; set; } = VerticalWrapMode.Overflow;
        public virtual bool bestFit { get; set; } = false;
        public virtual Color fontColor { get; set; } = Color.black;
        public virtual Material material { get; set; } = null;
        public virtual bool raycastTarget { get; set; } = true;
        public virtual Vector4 raycastPadding { get; set; } = Vector4.zero;
        public virtual bool maskable { get; set; } = true;


        internal Text Set(Text c)
        {
            c.font = font;
            c.text = text;
            c.fontStyle = fontStyle;
            c.fontSize = fontSize;
            c.lineSpacing = lineSpacing;
            c.supportRichText = richText;
            c.alignment = alignment;
            c.alignByGeometry = alignByGeometry;
            c.horizontalOverflow = horizontalOverflow;
            c.verticalOverflow = verticalOverflow;
            c.resizeTextForBestFit = bestFit;
            c.color = fontColor;
            c.material = material;
            c.raycastTarget = raycastTarget;
            c.raycastPadding = raycastPadding;
            c.maskable = maskable;
            
            return c;
        }

        internal Text Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Text>());
        }

        internal void SetListeners(Text c, TSelector selector)
        {
            c.onCullStateChanged.AddListener((b) =>
            {
                try
                {
                    OnCullStateChangedListener?.Invoke(b, selector);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error Executing OnCullStateChangedListener: " + e);
                }
            });


        }

    }

}
