using System.Collections;
using System.Collections.Generic;
using U.Reactor;
using UnityEngine;

public class eexp_VerticalDiv : MonoBehaviour
{
    // Select wich component will be drawed
    [Space(10)]
    public TestCase testCase = TestCase.A00Basic;
    public enum TestCase
    {
        A00Basic,
        A01WithChilds,
        A10WithScrollbar,
        A02ControlChildsWidth,
        A03BadWayRenderNoLayoutElements,
        A04RightWayToRenderNoLayoutElements,

    }

    // Destroy some elements OnStart
    [Space(10)]
    public bool destroyOnStart = false;
    public GameObject[] toDestroy;

    private TestCase lastTestCase;


    private void Update()
    {
        if (lastTestCase != testCase)
        {
            REcanvas.EraseAll();
            Start();
        }

    }

    private void Start()
    {

        if (toDestroy != null && destroyOnStart)
        {
            for (int i = 0; i < toDestroy.Length; i++)
            {
                if (toDestroy[i] == null) continue;

                UnityEngine.GameObject.Destroy(toDestroy[i]);
            }
        }

        switch (testCase)
        {

            case TestCase.A00Basic:
                #region TestCase.A00Basic

                new REcanvas
                {
                    childs = () => new REbase[]
                    {
                        new REpanelVertical
                        {
                        },
                    }

                }.Draw();

                #endregion
                break;

            case TestCase.A01WithChilds:
                #region TestCase.A01WithChilds

                new REcanvas
                {
                    childs = () => new REbase[]
                    {
                        new REpanelVertical
                        {
                            childs = () => new REbase[]
                            {
                                new REimage
                                {

                                },
                                new REimage
                                {

                                },
                            },
                        },
                    }

                }.Draw();

                #endregion
                break;

            case TestCase.A10WithScrollbar:
                #region TestCase.A10WithScrollbar

                new REcanvas
                {
                    childs = () => new REbase[]
                    {
                        new REpanelVertical
                        {
                            childs = () =>
                            {
                                var ch = new REbase[15];
                                for (int i = 0; i < ch.Length; i++)
                                {
                                    ch[i] = new REimage
                                    {

                                    };
			                    }
                                return ch;
                            },
                        },
                    }

                }.Draw();

                #endregion
                break;

            case TestCase.A02ControlChildsWidth:
                #region TestCase.A02ControlChildsWidth

                new REcanvas
                {
                    childs = () => new REbase[]
                    {
                        new REpanelVertical
                        {
                            propsVerticalLayoutGroup = () => new REpanelVertical.VerticalLayoutGroupSetter
                            {
                                childControlWidth = true,
                            },
                            childs = () => new REbase[]
                            {
                                new REimage
                                {
                                },
                                new REimage
                                {
                                },
                                new REimage
                                {
                                },
                                new REimage
                                {
                                },
                                new REimage
                                {
                                },
                            },
                        },
                    }

                }.Draw();

                #endregion
                break;

            case TestCase.A03BadWayRenderNoLayoutElements:
                #region TestCase.A03BadWayRenderNoLayoutElements

                new REcanvas
                {
                    childs = () => new REbase[]
                    {
                        new REpanelVertical
                        {
                            childs = () => new REbase[]
                            {
                                new REimage
                                {
                                    propsLayoutElement = () => new REimage.LayoutElementSetter
                                    {
                                        preferredHeight = 25,
                                    },
                                },
                                // This element wont de created and a error message will be displayed
                                new REpanelHorizontal
                                {
                                    
                                },
                                new REbox
                                {
                                    propsLayoutElement = () => new REbox.LayoutElementSetter
                                    {
                                        preferredHeight = 50,
                                    },
                                },
                                new REimage
                                {
                                    propsLayoutElement = () => new REimage.LayoutElementSetter
                                    {
                                        preferredHeight = 50,
                                    },
                                },
                            },
                        },
                    }

                }.Draw();

                #endregion
                break;

            case TestCase.A04RightWayToRenderNoLayoutElements:
                #region TestCase.A04RightWayToRenderNoLayoutElements

                new REcanvas
                {
                    childs = () => new REbase[]
                    {
                        new REpanelVertical
                        {
                            childs = () => new REbase[]
                            {
                                new REimage
                                {
                                    propsLayoutElement = () => new REimage.LayoutElementSetter
                                    {
                                        preferredHeight = 25,
                                    },
                                },
                                // Create inside a box
                                new REbox
                                {
                                    propsLayoutElement = () => new REbox.LayoutElementSetter
                                    {
                                        preferredHeight = 50,
                                    },
                                    childs = () => new REbase[]
                                    {
                                        new REpanelHorizontal
                                        {

                                        },
                                    },
                                },
                                new REbox
                                {
                                    propsLayoutElement = () => new REbox.LayoutElementSetter
                                    {
                                        preferredHeight = 50,
                                    },
                                },
                                new REimage
                                {
                                    propsLayoutElement = () => new REimage.LayoutElementSetter
                                    {
                                        preferredHeight = 50,
                                    },
                                },
                            },
                        },
                    }

                }.Draw();

                #endregion
                break;


        }

        lastTestCase = testCase;
    }
}
