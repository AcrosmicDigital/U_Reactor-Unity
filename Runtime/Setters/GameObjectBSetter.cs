using System;
using UnityEngine;

namespace U.Reactor
{
    /// <summary>
    /// Set to a GameObject default values in Unity v2020.3.1f1
    /// </summary>
    public class GameObjectBSetter
    {
        // Listeners
        // ...
        // Properties
        public virtual string name { get; set; } = "GameObject"; // 
        public virtual int layer { get; set; } = 5; // In what layer the GameObject Should Be, default is 5 = UI layer
        public virtual bool active { get; set; } = true;  // If the GO will be enabled by default
        public virtual string tag { get; set; } = null;
        public virtual bool dontDestroyOnLoad { get; set; } = false;


        internal GameObject SetNameLayerAndTag(GameObject c)
        {
            c.name = name;
            c.layer = layer;

            // Set the tag
            if (!String.IsNullOrEmpty(tag) && !String.IsNullOrWhiteSpace(tag))
                c.tag = tag;


            return c;
        }

        internal GameObject SetDontDestroyOnLoad(GameObject c)
        {
            if (dontDestroyOnLoad && c.transform.parent != null)
                UnityEngine.Object.DontDestroyOnLoad(c);


            return c;
        }
    }

}
