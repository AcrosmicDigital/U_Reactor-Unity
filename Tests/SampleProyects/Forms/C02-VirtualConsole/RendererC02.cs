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

        var addChildToConsole = new UseAddChilds();

        REbase TextChild(string text)
        {
            return new REbox
            {
                propsRectTransform = () => new REbox.RectTransformSetter
                {
                    height = 200,
                    width = 800,
                },
                childs = () => new REbase[]
                {
                    new REhorizontalLayout
                    {
                        childs = () => new REbase[]
                        {
                            new RElabel
                            {
                                propsText = () => new RElabel.TextSetter
                                {
                                    text = text,
                                }
                            },
                            new REbutton
                            {
                                propsText = () => new REbutton.TextSetter
                                {
                                    text = "Delete",
                                }
                            },
                        }
                    },
                }
            };
        }


        return new REcanvas
        {
            childs = () => new REbase[]
            {
                new REverticalLayout
                {
                    propsRectTransform = () => REverticalLayout.TableRectTransform(0,100,40,100),
                    useAddChilds = new IuseAddChilds[]
                    {
                        addChildToConsole,
                    },
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
                                    },
                                    propsButton = () => new REbutton.ButtonSetter
                                    {
                                        OnClickListener = (s) => addChildToConsole.AddChild(TextChild("Lolloolll")),
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
