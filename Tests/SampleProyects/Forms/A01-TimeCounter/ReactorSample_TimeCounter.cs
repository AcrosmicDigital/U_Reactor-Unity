using System.Collections;
using System.Collections.Generic;
using U.Reactor;
using UnityEngine;

public class ReactorSample_TimeCounter : MonoBehaviour
{

    private float time = 0;
    public bool isPaused = false;


    void Start()
    {

        // Render component
        ListREcomponent().Draw();

    }

    private void Update()
    {

        if (isPaused) return;

        time += Time.deltaTime;

    }



    private REcanvas ListREcomponent()
    {

        return new REcanvas
        {
            childs = () => new REbase[]
            {
                new REpanelVertical
                {
                    propsVerticalLayoutGroup = () => new REpanelVertical.VerticalLayoutGroupSetter
                    {
                        childAlignment = TextAnchor.MiddleCenter,
                    },
                    childs = () => new REbase[]
                    {
                        new REbox{},
                        new REtext
                        {
                            propsRectTransform = () => new REtext.RectTransformSetter
                            {
                               localPosition = new Vector3(0,200),
                            },
                            propsText = () => new REtext.TextSetter
                            {
                                text = "Time: ",
                            }
                        },
                        new REtext
                        {
                            propsText = () => new REtext.TextSetter
                            {
                                text = "",
                            },
                            useEffect = new REtext.UseEffect.Hook[]
                            {
                                new REtext.UseEffect.Hook
                                {
                                    deltaFunction = (d,s) =>
                                    {
                                        s.textCmp.text = "" + time.ToString("0.00");
                                    },
                                    duration = .2f,
                                }
                            }
                        },
                        new REbutton
                        {
                            propsText = () =>
                            {
                                if(isPaused)
                                    return new REbutton.TextSetter
                                    {
                                        text = "Play",
                                    };
                                else
                                    return new REbutton.TextSetter
                                    {
                                        text = "Pause",
                                    };
                            },
                            propsButton = ()=> new REbutton.ButtonSetter
                            {
                                OnClickListener = (s) =>
                                {
                                    isPaused = !isPaused;

                                    if(isPaused) s.textCmp.text = "Play";
                                    else s.textCmp.text = "Pause";
                                },
                            }
                        },
                        new REbutton
                        {
                            propsText = () => new REbutton.TextSetter
                            {
                                text = "Reset",
                            },
                            propsButton = ()=> new REbutton.ButtonSetter
                            {
                                OnClickListener = (s) =>
                                {
                                    time = 0;
                                },
                            }
                        },
                    },
                },
            }
        };
    }

}

