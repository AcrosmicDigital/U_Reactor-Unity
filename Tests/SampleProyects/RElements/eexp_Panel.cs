using System.Collections;
using System.Collections.Generic;
using U.Reactor;
using UnityEngine;

public class eexp_Panel : MonoBehaviour
{
    // Select wich component will be drawed
    [Space(10)]
    public TestCase testCase = TestCase.A00Basic;
    public enum TestCase
    {
        A00Basic,
        A01NestedPanels,

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
                        new REpanel
                        {
                        },
                    }

                }.Draw();

                #endregion
                break;

            case TestCase.A01NestedPanels:
                #region TestCase.A01NestedPanels

                new REcanvas
                {
                    childs = () => new REbase[]
                    {
                        new REpanel
                        {
                            childs = () => new REbase[]
                            {
                                new REpanel
                                {
                                    childs = () => new REbase[]
                                    {
                                        new REpanel
                                        {
                                        },
                                    }
                                },
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
