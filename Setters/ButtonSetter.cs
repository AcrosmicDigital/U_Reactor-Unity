using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace U.Reactor
{
    public class ButtonSetter
    {
        public virtual UnityAction OnClickListener { get; set; } = () => { };
        public virtual bool interactable { get; set; } = true;
        public virtual Selectable.Transition transition { get; set; } = Selectable.Transition.ColorTint;
        //public Navigation navigation = ;

        public Button Set(Button c)
        {
            c.interactable = interactable;
            c.transition = transition;
            //c.navigation = navigation;
            c.onClick.AddListener(OnClickListener);

            return c;
        }

        public Button Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Button>());
        }

    }

}
