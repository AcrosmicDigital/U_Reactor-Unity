using System.Collections;
using System.Collections.Generic;
using U.Reactor;
using UnityEngine;

public class eexp_Slider : MonoBehaviour
{
    // Select wich component will be drawed
    [Space(10)]
    public TestCase testCase = TestCase.A00Basic;
    public enum TestCase
    {
        A00Basic,
        A00WithEvent,
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
                        new REslider
                        {
                        },
                    }

                }.Draw();

                #endregion
                break;

            case TestCase.A00WithEvent:
                #region TestCase.A00WithEvent

                new REcanvas
                {
                    childs = () => new REbase[]
                    {
                        new REslider
                        {
                            propsSlider = () => new REslider.SliderSetter
                            {
                                OnValueChangedListener = (v, s) => 
                                {
                                    Debug.Log("Slider: " + s.gameObject.name + " Value: " + v);
                                }
                            }
                        },
                    }

                }.Draw();

                #endregion
                break;

        }
    }
}
