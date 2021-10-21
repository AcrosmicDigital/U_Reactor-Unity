using UnityEngine;

namespace U.Reactor
{
    public class CanvasGroupBSetter
    {
        // Listeners
        // ...
        // Properties
        public virtual float alpha { get; set; } = 1;
        public virtual bool interactable { get; set; } = true;
        public virtual bool blocksRaycasts { get; set; } = true;
        public virtual bool ignoreParentGroups { get; set; } = false;

        public CanvasGroup Set(CanvasGroup c)
        {
            c.alpha = alpha;
            c.interactable = interactable;
            c.blocksRaycasts = blocksRaycasts;
            c.ignoreParentGroups = ignoreParentGroups;

            return c;
        }

        public CanvasGroup Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<CanvasGroup>());
        }
    }
}
