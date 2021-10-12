using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class SliderSetter
    {
        public Slider Set(Slider c)
        {


            return c;
        }


        public Slider Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Slider>());
        }
    }
}
