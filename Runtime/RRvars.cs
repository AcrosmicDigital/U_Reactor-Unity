using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U.Reactor
{
    public static partial class RRvars
    {
        public static class Anchors
        {
            public static class Min
            {
                public static readonly Vector2 topLeft = new Vector2(0, 1);
                public static readonly Vector2 topMiddle = new Vector2(0.5f, 1);
                public static readonly Vector2 topRight = new Vector2(1, 1);
                public static readonly Vector2 middleLeft = new Vector2(0, 0.5f);
                public static readonly Vector2 middle = new Vector2(0.5f, 0.5f);
                public static readonly Vector2 middleRight = new Vector2(1, 0.5f);
                public static readonly Vector2 bottomLeft = new Vector2(0, 0);
                public static readonly Vector2 bottomMiddle = new Vector2(0.5f, 0);
                public static readonly Vector2 botttomRight = new Vector2(1, 0);

                public static readonly Vector2 stretchTop = new Vector2(0, 1);
                public static readonly Vector2 stretchMiddle = new Vector2(0, 0.5f);
                public static readonly Vector2 stretchBottom = new Vector2(0, 0);

                public static readonly Vector2 leftStretch = new Vector2(0, 0);
                public static readonly Vector2 middleStretch = new Vector2(0.5f, 0);
                public static readonly Vector2 rightStretch = new Vector2(1, 0);

                public static readonly Vector2 expand = new Vector2(0, 0);
            }

            public static class Max
            {
                public static readonly Vector2 topLeft = new Vector2(0, 1);
                public static readonly Vector2 topMiddle = new Vector2(0.5f, 1);
                public static readonly Vector2 topRight = new Vector2(1, 1);
                public static readonly Vector2 middleLeft = new Vector2(0, 0.5f);
                public static readonly Vector2 middle = new Vector2(0.5f, 0.5f);
                public static readonly Vector2 middleRight = new Vector2(1, 0.5f);
                public static readonly Vector2 bottomLeft = new Vector2(0, 0);
                public static readonly Vector2 bottomMiddle = new Vector2(0.5f, 0);
                public static readonly Vector2 botttomRight = new Vector2(1, 0);

                public static readonly Vector2 stretchTop = new Vector2(1, 1);
                public static readonly Vector2 stretchMiddle = new Vector2(1, 0.5f);
                public static readonly Vector2 stretchBottom = new Vector2(1, 0);

                public static readonly Vector2 leftStretch = new Vector2(0, 1);
                public static readonly Vector2 middleStretch = new Vector2(0.5f, 1);
                public static readonly Vector2 rightStretch = new Vector2(1, 1);

                public static readonly Vector2 expand = new Vector2(1, 1);
            }
        }
    }
}
