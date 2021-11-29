using UnityEngine;

namespace U.Reactor
{
    /// <summary>
    /// Create a HC.ReactorId in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class IdBSetter
    {
        // Listeners
        // ...
        // Properties
        public virtual string id { get; set; } = "";
        public virtual string[] className { get; set; } = new string[0];


        internal HC.ReactorId Set(HC.ReactorId c)
        {
            c.id = id;
            c.className = className;

            return c;
        }

        internal HC.ReactorId Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<HC.ReactorId>());
        }

    }

}
