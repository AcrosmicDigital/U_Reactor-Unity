using System.Collections;
using System.Collections.Generic;
using U.Reactor;
using UnityEngine;

public class eexp_Canvas : MonoBehaviour
{
    // Select wich component will be drawed
    [Space(10)]
    public TestCase testCase = TestCase.A00Basic;
    public enum TestCase
    {
        A00Basic,
        A01NestedCanvas,

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
                }.Draw();

                #endregion
                break;

            case TestCase.A01NestedCanvas:
                #region TestCase.A01NestedCanvas

                new REcanvas
                {
                    childs = () => new REbase[]
                    {
                        new REcanvas
                        {
                            childs = () => new REbase[]
                            {
                                new REcanvas
                                {
                                    childs = () => new REbase[]
                                    {
                                        new REcanvas
                                        {

                                        }
                                    }
                                }
                            }
                        }
                    }
                }.Draw();

                #endregion
                break;

        }

        lastTestCase = testCase;
    }
}
