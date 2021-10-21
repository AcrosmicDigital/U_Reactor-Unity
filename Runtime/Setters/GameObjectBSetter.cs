using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U.Reactor
{
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
    }

}
