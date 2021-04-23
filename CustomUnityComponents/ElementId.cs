using UnityEngine;

namespace U.Reactor
{
    // Class to have a reference to the object, added to base of all components
    public class ElementId : MonoBehaviour
    {
        public string id { get; private set; } = "";
        public string[] className { get; private set; } = new string[0];
        public ElementSelector selector { get; private set; } = null;


        internal ElementId Set(string id, string[] className)
        {

            this.id = id;
            this.className = className;
            this.selector = selector;

            return this;
        }

        internal void Set(ElementSelector selector)
        {
            this.selector = selector;
        }

        private void OnDestroy()
        {
            if (selector != null)
                selector.Destroy();
        }
    }
}
