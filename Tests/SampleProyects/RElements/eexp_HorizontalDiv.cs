using System.Collections;
using System.Collections.Generic;
using U.Reactor;
using UnityEngine;

public class eexp_HorizontalDiv : MonoBehaviour
{
    // Select wich component will be drawed
    [Space(10)]
    public TestCase testCase = TestCase.A00Basic;
    public enum TestCase
    {
        A00Basic,
        A01WithChilds,

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
                        new REhorizontalLayout
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
                        new REhorizontalLayout
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

                                }
                            },
                        },
                    }

                }.Draw();

                #endregion
                break;

        }
    }
}