using System.Collections;
using System.Collections.Generic;
using U.Reactor;
using UnityEngine;

public class eexp_Button : MonoBehaviour
{
    // Select wich component will be drawed
    [Space(10)]
    public TestCase testCase = TestCase.A00Basic;
    public enum TestCase
    {
        A00Basic,
        A01WithAction,

        S00ChangeWidth,
        S01ChangeHeigh,
        S02DoubleSize,
        S03HalfSize,

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

        if(toDestroy != null && destroyOnStart)
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
                        new REbutton
                        {

                        },
                    }

                }.Draw();

                #endregion
                break;

            case TestCase.A01WithAction:
                #region TestCase.A01WithAction

                new REcanvas
                {
                    childs = () => new REbase[]
                    {
                        new REbutton
                        {
                            propsButton = () => new REbutton.ButtonSetter
                            {
                                OnClickListener = (s) => 
                                {
                                    Debug.Log("Pressed: " + s.gameObject.name);
                                }
                            },
                        },
                    }

                }.Draw();

                #endregion
                break;


            // SXX - reference 370,60

            case TestCase.S00ChangeWidth:
                #region TestCase.S00ChangeWidth

                new REcanvas
                {
                    childs = () => new REbase[]
                    {
                        new REbutton
                        {
                            propsRectTransform = () => new REbutton.RectTransformSetter
                            {
                                width = 600,
                                height = 200,
                            }
                        },
                    }

                }.Draw();

                #endregion
                break;

            case TestCase.S01ChangeHeigh:
                #region TestCase.S01ChangeHeigh

                new REcanvas
                {
                    childs = () => new REbase[]
                    {
                        new REbutton
                        {
                            propsRectTransform = () => new REbutton.RectTransformSetter
                            {
                                width = 200,
                                height = 600,
                            }
                        },
                    }

                }.Draw();

                #endregion
                break;

            case TestCase.S02DoubleSize:
                #region TestCase.S02DoubleSize

                new REcanvas
                {
                    childs = () => new REbase[]
                    {
                        new REbutton
                        {
                            propsRectTransform = () => new REbutton.RectTransformSetter
                            {
                                width = 740,
                                height = 120,
                            }
                        },
                    }

                }.Draw();

                #endregion
                break;

            case TestCase.S03HalfSize:
                #region TestCase.S03HalfSize

                new REcanvas
                {
                    childs = () => new REbase[]
                    {
                        new REbutton
                        {
                            propsRectTransform = () => new REbutton.RectTransformSetter
                            {
                                width = 185,
                                height = 30,
                            }
                        },
                    }

                }.Draw();

                #endregion
                break;

        }

        lastTestCase = testCase;
    }
}
