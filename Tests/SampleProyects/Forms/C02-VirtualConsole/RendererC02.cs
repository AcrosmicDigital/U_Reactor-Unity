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
        string inputText = "";

        REbase TextChild(string text)
        {
            string displayText = text;
            var editMode = new UseState<bool>(false);

            return new REbox
            {
                propsRectTransform = () => new REbox.RectTransformSetter
                {
                    height = 200,
                    width = 800,
                },
                childs = () => new REbase[]
                {
                    new REpanelHorizontal
                    {
                        childs = () =>
                        {
                            return new REbase[]
                            {
                                new REbox
                                {
                                    propsRectTransform = () => new REbox.RectTransformSetter
                                    {
                                        width = 400,
                                        height = 200,
                                    },
                                    useState = new IuseState[]
                                    {
                                        editMode,
                                    },
                                    childs = () => 
                                    {
                                        if (editMode.value)
                                        {
                                            return new REbase[]
                                            {
                                                new REinputField
                                                {
                                                    propsInputField = () => new REinputField.InputFieldSetter
                                                    {
                                                        text = displayText,
                                                        OnValueChangedListener = (v,s) => 
                                                        {
                                                            displayText = v;
                                                        }
                                                    },
                                                },
                                            };
                                        }
                                        else
                                        {
                                            return new REbase[]
                                            {
                                                new RElabel
                                                {
                                                    propsText = () => new RElabel.TextSetter
                                                    {
                                                        text = displayText,
                                                    },
                                                },
                                            };
                                        }
                                    }
                                },
                                new REbutton
                                {
                                    propsText = () =>
                                    {
                                        if(editMode.value) return new REbutton.TextSetter
                                        {
                                            text = "Save"
                                        };
                                        else return new REbutton.TextSetter
                                        {
                                            text = "Edit"
                                        };
                                    },
                                    propsButton = () => new REbutton.ButtonSetter
                                    {
                                        OnClickListener = (s) =>
                                        {
                                            editMode.SetState(!editMode.value);
                                        }
                                    },
                                    useState = new IuseState[]
                                    {
                                        editMode,
                                    },
                                },
                                new REbutton
                                {
                                    propsText = () => new REbutton.TextSetter
                                    {
                                        text = "Delete",
                                    },
                                    propsButton = () => new REbutton.ButtonSetter
                                    {
                                        OnClickListener = (s) =>
                                        {
                                            Debug.Log(s.parent.parent.elementId.elementType + " r " + s.parent.parent.parent.childs.Length);
                                            s.parent.parent.Erase();
                                        }
                                    },
                                },
                            };
                        }
                    },
                }
            };
        }


        return new REcanvas
        {
            childs = () => new REbase[]
            {
                new REpanelVertical
                {
                    propsRectTransform = () => REpanelVertical.TableRectTransform(0,100,40,100),
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
                        new REpanelVertical
                        {
                            propsRectTransform = () => REpanelVertical.TableRectTransform(0,30,0,100),
                            childs = () => new REbase[]
                            {
                                new REbutton{
                                    propsText = () => new REbutton.TextSetter
                                    {
                                        text = "Add",
                                    },
                                    propsButton = () => new REbutton.ButtonSetter
                                    {
                                        OnClickListener = (s) => 
                                        {
                                            addChildToConsole.AddChild(TextChild(inputText));
                                            inputText = "";
                                        },
                                    }
                                },

                                new REbutton{
                                    propsText = () => new REbutton.TextSetter
                                    {
                                        text = "Clear",
                                    },
                                    propsButton = () => new REbutton.ButtonSetter
                                    {
                                        OnClickListener = (s) =>
                                        {

                                        },
                                    }
                                }
                            }
                        },
                        new REpanel
                        {
                            propsRectTransform = () => REpanel.TableRectTransform(33,100,0,100),
                            childs = () => new REbase[]
                            {
                                new REinputField
                                {
                                    propsRectTransform = () => new REinputField.RectTransformSetter
                                    {
                                        width = 670,
                                        height = 400,
                                    },
                                    propsInputField = () => new REinputField.InputFieldSetter
                                    {
                                        lineType = UnityEngine.UI.InputField.LineType.MultiLineNewline,
                                        OnValueChangedListener = (v,s) => {inputText = v; }
                                    }
                                    
                                },
                            }
                        },
                    }
                },
            }
        };
    }

}
