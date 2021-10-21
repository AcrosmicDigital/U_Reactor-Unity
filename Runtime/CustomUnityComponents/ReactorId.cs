using System;
using UnityEngine;

namespace U.Reactor
{
    // Class to have a reference to the object, added to base of all components
    public class ReactorId : MonoBehaviour
    {
        public Type elementType { get; private set; }
        public string id { get; private set; } = "";
        public string[] className { get; private set; } = new string[0];
        public REbaseSelector selector { get; private set; } = null;


        internal ReactorId Set(Type elementType, string id, string[] className)
        {
            this.elementType = elementType;
            this.id = id;
            this.className = className;
            this.selector = selector;

            return this;
        }

        internal void Set(REbaseSelector selector)
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
