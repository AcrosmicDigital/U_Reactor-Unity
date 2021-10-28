using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U.Reactor
{
    public partial class ReactorCmd
    {
        private static List<REcanvas> drawedCanvas = new List<REcanvas>();
        internal static void AddToReactorCmd(REcanvas element)
        {
            drawedCanvas.Add(element);
        }
        internal static void RemoveFromReactorCmd(REcanvas element)
        {
            drawedCanvas.Remove(element);
        }


        public static Sprite GetSprite(string path)
        {
            return Resources.Load<Sprite>("Reactor/Sprites/" + path);
        }

        public static Font GetFont(string path)
        {
            return Resources.Load<Font>("Reactor/Sprites/" + path);
        }

        public static void EraseAll()
        {
            for (int i = 0; i < drawedCanvas.Count; i++)
            {
                drawedCanvas[i].Erase();
            }

        }

    }
}
