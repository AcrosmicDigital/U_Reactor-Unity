using System;
using UnityEngine;

namespace U.Reactor
{
    public static partial class HC
    {
        public class ReactorId : MonoBehaviour
        {
            
            // To show in inspector
            public string id = "";
            public string[] className = new string[0];

            public Type elementType { get; private set; }
            internal REbaseSelector selector { get; private set; }


            internal ReactorId SetElementType(Type elementType)
            {
                this.elementType = elementType;

                return this;
            }

            internal void SetSelector(REbaseSelector selector)
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
}
