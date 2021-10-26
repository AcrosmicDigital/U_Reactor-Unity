using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class GridLayoutGroupBSetter
    {
        // Listeners
        // ...
        // Properties
        public virtual RectOffset padding { get; set; } = new RectOffset(0, 0, 0, 0);
        public virtual Vector2 cellSize { get; set; } = new Vector2(100, 100);
        public virtual Vector2 spacing { get; set; } = Vector2.zero;
        public virtual GridLayoutGroup.Corner startCorner { get; set; } = GridLayoutGroup.Corner.UpperLeft;
        public virtual GridLayoutGroup.Axis startAxis { get; set; } = GridLayoutGroup.Axis.Horizontal;
        public virtual TextAnchor childAlignment { get; set; } = TextAnchor.UpperLeft;
        public virtual GridLayoutGroup.Constraint constraint { get; set; } = GridLayoutGroup.Constraint.Flexible;
        public virtual int constraintCount { get; set; } = 2;


        internal GridLayoutGroup Set(GridLayoutGroup c)
        {
            c.padding = padding;
            c.cellSize = cellSize;
            c.spacing = spacing;
            c.startCorner = startCorner;
            c.startAxis = startAxis;
            c.childAlignment = childAlignment;
            c.constraint = constraint;
            c.constraintCount = constraintCount;


            return c;
        }


        internal GridLayoutGroup Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<GridLayoutGroup>());
        }
    }
}
