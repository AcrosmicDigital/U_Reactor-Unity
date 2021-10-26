using UnityEngine;

namespace U.Reactor
{
    /// <summary>
    /// Create a CanvasGroup in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class CanvasGroupBSetter
    {
        // Listeners
        // ...
        // Properties
        public virtual float alpha { get; set; } = 1;
        public virtual bool interactable { get; set; } = true;
        public virtual bool blocksRaycasts { get; set; } = true;
        public virtual bool ignoreParentGroups { get; set; } = false;

        internal CanvasGroup Set(CanvasGroup c)
        {
            c.alpha = alpha;
            c.interactable = interactable;
            c.blocksRaycasts = blocksRaycasts;
            c.ignoreParentGroups = ignoreParentGroups;

            return c;
        }

        internal CanvasGroup Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<CanvasGroup>());
        }
    }
}
