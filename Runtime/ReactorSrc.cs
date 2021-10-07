using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U.Reactor
{
    public partial class ReactorSrc
    {

        public static Sprite GetSprite(string path)
        {
            return Resources.Load<Sprite>("Reactor/Sprites/" + path);
        }

        public static Font GetFont(string path)
        {
            return Resources.Load<Font>("Reactor/Sprites/" + path);
        }

    }
}
