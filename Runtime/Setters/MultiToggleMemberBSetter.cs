﻿using UnityEngine;

namespace U.Reactor
{
    /// <summary>
    /// Create a HC.MultiToggleMember in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class MultiToggleMemberBSetter
    {
        // Listeners
        // ...
        // Properties
        public virtual string toggleName { get; set; } = "Toggle";
        public virtual int toggleNumber { get; set; } = 0;
        public virtual float toggleValue { get; set; } = 0f;


        internal HC.MultiToggleMember Set(HC.MultiToggleMember c)
        {
            c.toggleName = toggleName;
            c.toggleNumber = toggleNumber;
            c.toggleValue = toggleValue;

            return c;
        }


        internal HC.MultiToggleMember Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<HC.MultiToggleMember>());
        }
    }
}
