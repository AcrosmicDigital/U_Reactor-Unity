using System.Collections;
using System.Collections.Generic;
using U.Reactor;
using UnityEngine;

public class RendererC02 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Render component
        ConsoleView().Draw();
    }


    private REcanvas ConsoleView()
    {
        return new REcanvas
        {
            childs = () => new REbase[]
            {
                new REverticalLayout
                {
                    propsRectTransform = () => REverticalLayout.TableRectTransform(0,100,40,100),
                },
                new REdiv
                {
                    propsRectTransform = () => REdiv.TableRectTransform(0,100,0,38),
                    childs = () => new REbase[]
                    {
                        new REverticalLayout
                        {
                            propsRectTransform = () => REverticalLayout.TableRectTransform(0,30,0,100),
                            childs = () => new REbase[]
                            {
                                new REbutton{
                                    propsText = () => new REbutton.TextSetter
                                    {
                                        text = "Add",
                                    }
                                },

                                new REbutton{
                                    propsText = () => new REbutton.TextSetter
                                    {
                                        text = "Remove",
                                    }
                                }
                            }
                        },
                        new REpanel
                        {
                            propsRectTransform = () => REpanel.TableRectTransform(33,100,0,100),
                        },
                    }
                },
            }
        };
    }

}
