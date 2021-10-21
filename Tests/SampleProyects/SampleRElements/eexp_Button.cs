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

    }

    // Destroy some elements OnStart
    [Space(10)]
    public bool destroyOnStart = false;
    public GameObject[] toDestroy;


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

        }
    }
}
