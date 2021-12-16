using System.Collections;
using System.Collections.Generic;
using U.Reactor;
using UnityEngine;

public class ToDnnn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var bb = new REcanvas
        {
            propsGameObject = () => new REcanvas.GameObjectSetter
            {

            },
        }.Draw();

        bb.ToDontDestroyOnLoad();
    }

}
