using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class ScrollbarSetter
    {
        public bool interactable = true;
        public Selectable.Transition transition = Selectable.Transition.ColorTint;
        public Navigation navigation = new Navigation 
        { 
            mode = Navigation.Mode.Automatic,  

        };
        public Scrollbar.Direction direction = Scrollbar.Direction.LeftToRight;
        public float value = 0;
        public float size = .2f;
        public int numberOfSteps = 0;

        public Scrollbar Set(Scrollbar c)
        {
            c.interactable = interactable;
            c.transition = transition;
            c.navigation = navigation;
            c.direction = direction;
            c.value = value;
            c.size = size;
            c.numberOfSteps = numberOfSteps;

            return c;
        }


        public Scrollbar Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Scrollbar>());
        }
    }
}
