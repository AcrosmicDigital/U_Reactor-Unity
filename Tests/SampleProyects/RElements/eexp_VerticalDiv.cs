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
            ReactorCmd.EraseAll();
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
                        new REverticalLayout
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
                        new REverticalLayout
                        {
                            childs = () => new REbase[]
                            {
                                new REimage
                                {

                                },
                                new REtext
                                {

                                },
                                new REimage
                                {

                                },
                                new REdropdown
                                {

                                },
                                new REimage
                                {

                                },
                                new REbutton
                                {

                                },
                                new REcanvas
                                {

                                },
                                new REhorizontalLayout
                                {

                                },
                                new REpanel
                                {

                                },
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
                        new REverticalLayout
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
                                new REimage
                                {
                                    propsLayoutElement = () => new REimage.LayoutElementSetter
                                    {
                                        preferredHeight = 50,
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
                                new REbox
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
                        new REverticalLayout
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
                                new REhorizontalLayout
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
                        new REverticalLayout
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
                                        new REhorizontalLayout
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
