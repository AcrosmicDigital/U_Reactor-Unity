using System.Collections;
using System.Collections.Generic;
using U.Reactor;
using UnityEngine;

public class eexp_MultiToggle : MonoBehaviour
{
    // Select wich component will be drawed
    [Space(10)]
    public TestCase testCase = TestCase.A00Basic;
    public enum TestCase
    {
        A00Basic,
        A01WithChilds,
        A02WithChildsInDiv,

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
                        new REmultiToggle
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
                        new REmultiToggle
                        {
                            propsMultiToggle = () => new REmultiToggle.MultiToggleSetter
                            {
                                maxEnabled = 2,
                            },
                            childs = () => new REbase[]
                            {
                                new REtoggle
                                {
                                    propsRectTransform = () => new REtoggle.RectTransformSetter
                                    {
                                        localPosition = new Vector3(0, 200)
                                    },
                                    propsMultiToggleMember = () => new REtoggle.MultiToggleMemberSetter
                                    {
                                        toggleName = "One",
                                        toggleNumber = 1,
                                        toggleValue = 1.1f
                                    },
                                },
                                new REtoggle
                                {
                                    propsRectTransform = () => new REtoggle.RectTransformSetter
                                    {
                                        localPosition = new Vector3(0, 100)
                                    },
                                    propsMultiToggleMember = () => new REtoggle.MultiToggleMemberSetter
                                    {
                                        toggleName = "Two",
                                        toggleNumber = 2,
                                        toggleValue = 2.2f
                                    },
                                },
                                new REtoggle
                                {
                                    propsRectTransform = () => new REtoggle.RectTransformSetter
                                    {
                                        localPosition = new Vector3(0, 0)
                                    },
                                    propsMultiToggleMember = () => new REtoggle.MultiToggleMemberSetter
                                    {
                                        toggleName = "Three",
                                        toggleNumber = 3,
                                        toggleValue = 3.3f
                                    },
                                },
                                new REtoggle
                                {
                                    propsRectTransform = () => new REtoggle.RectTransformSetter
                                    {
                                        localPosition = new Vector3(0, -100)
                                    },
                                    propsMultiToggleMember = () => new REtoggle.MultiToggleMemberSetter
                                    {
                                        toggleName = "Four",
                                        toggleNumber = 4,
                                        toggleValue = 4.4f
                                    },
                                },
                            },
                        },
                    }

                }.Draw();

                #endregion
                break;

            case TestCase.A02WithChildsInDiv:
                #region TestCase.A02WithChildsInDiv

                new REcanvas
                {
                    childs = () => new REbase[]
                    {
                        new REmultiToggle
                        {
                            propsMultiToggle = () => new REmultiToggle.MultiToggleSetter
                            {
                                maxEnabled = 2,
                            },
                            childs = () => new REbase[]
                            {
                                new REpanelVertical
                                {
                                    childs = () => new REbase[]
                                    {
                                        new REtoggle
                                        {
                                            propsRectTransform = () => new REtoggle.RectTransformSetter
                                            {
                                                localPosition = new Vector3(0, 200)
                                            },
                                            propsMultiToggleMember = () => new REtoggle.MultiToggleMemberSetter
                                            {
                                                toggleName = "One",
                                                toggleNumber = 1,
                                                toggleValue = 1.1f
                                            },
                                        },
                                        new REtoggle
                                        {
                                            propsRectTransform = () => new REtoggle.RectTransformSetter
                                            {
                                                localPosition = new Vector3(0, 100)
                                            },
                                            propsMultiToggleMember = () => new REtoggle.MultiToggleMemberSetter
                                            {
                                                toggleName = "Two",
                                                toggleNumber = 2,
                                                toggleValue = 2.2f
                                            },
                                        },
                                        new REtoggle
                                        {
                                            propsRectTransform = () => new REtoggle.RectTransformSetter
                                            {
                                                localPosition = new Vector3(0, 0)
                                            },
                                            propsMultiToggleMember = () => new REtoggle.MultiToggleMemberSetter
                                            {
                                                toggleName = "Three",
                                                toggleNumber = 3,
                                                toggleValue = 3.3f
                                            },
                                        },
                                        new REtoggle
                                        {
                                            propsRectTransform = () => new REtoggle.RectTransformSetter
                                            {
                                                localPosition = new Vector3(0, -100)
                                            },
                                            propsMultiToggleMember = () => new REtoggle.MultiToggleMemberSetter
                                            {
                                                toggleName = "Four",
                                                toggleNumber = 4,
                                                toggleValue = 4.4f
                                            },
                                        },
                                    },
                                }
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
