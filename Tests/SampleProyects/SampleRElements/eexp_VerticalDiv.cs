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

    }

    // Destroy some elements OnStart
    [Space(10)]
    public bool destroyOnStart = false;
    public GameObject[] toDestroy;


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
                        new REverticalDiv
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
                        new REverticalDiv
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
                                new REhorizontalDiv
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
                        new REverticalDiv
                        {
                            childs = () => new REbase[]
                            {
                                new REimage
                                {
                                    propsLayoutElement = () => new REbase.LayoutElementSetter
                                    {
                                        preferredHeight = 25,
                                    },
                                },
                                new REimage
                                {
                                    propsLayoutElement = () => new REbase.LayoutElementSetter
                                    {
                                        preferredHeight = 50,
                                    },
                                },
                                new REbox
                                {
                                    propsLayoutElement = () => new REbase.LayoutElementSetter
                                    {
                                        preferredHeight = 50,
                                    },
                                },
                                new REimage
                                {
                                    propsLayoutElement = () => new REbase.LayoutElementSetter
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

        }
    }
}
