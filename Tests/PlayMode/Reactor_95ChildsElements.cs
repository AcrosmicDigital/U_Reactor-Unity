using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using U.Reactor;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class Reactor_95ChildsElements
{

    GameObject mainCamera = null;
    GameObject eventSystem = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        mainCamera = new GameObject("Main Camera");
        var camera = mainCamera.AddComponent<Camera>();
        camera.backgroundColor = Color.grey;
        camera.orthographic = true;
        camera.depth = -1;
        mainCamera.AddComponent<AudioListener>();
        eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        UnityEngine.Object.Destroy(mainCamera);
        UnityEngine.Object.Destroy(eventSystem);
    }



    [UnityTest]
    public IEnumerator GenerateChildsFromArray()
    {
        var words = new string[]
        {
            "One",
            "Two",
            "Three",
            "Four",
            "Five",
        };

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => words.Select(c => 
                    new REtext 
                    {
                        propsReactorId = () => new REtext.ReactorIdSetter
                        {
                            id = c + "Num",
                        },
                        propsText = () => new REtext.TextSetter
                        {
                            text = c,
                        }
                    }
                ),

            };

        }

        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        Debug.Log("Drawing");
        routerProve.Draw();

        Debug.Log("Brothers");
        Assert.IsTrue(REtext.FindOne("#" + words[0] + "Num").brothersSelector.Length == 5);

        Debug.Log("Childs");
        foreach (var word in words)
        {
            //Debug.Log("Child: " + "#" + word + "Num" + " == " + word);
            //Debug.Log("Child: " + ReactorElement.Find<REtext.Selector>("#" + word + "Num")[0].textCmp.text + " == " + word);
            Assert.IsTrue(REtext.FindOne("#" + word + "Num").textCmp.text == word);
        }

        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }

    [UnityTest]
    public IEnumerator GenerateChildsFromDinamicList()
    {
        var words = new List<string>
        {
            "One",
            "Two",
            "Three",
            "Four",
            "Five",
        };

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => words.Select(c =>
                    new REtext
                    {
                        propsReactorId = () => new REtext.ReactorIdSetter
                        {
                            id = c + "Num",
                        },
                        propsText = () => new REtext.TextSetter
                        {
                            text = c,
                        }
                    }
                ),

            };

        }
        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        Debug.Log("Drawing");
        routerProve.Draw();

        // First check
        Debug.Log("Brothers");
        Assert.IsTrue(REtext.FindOne("#" + words[0] + "Num").brothersSelector.Length == 5);

        Debug.Log("Childs");
        foreach (var word in words)
        {
            //Debug.Log("Child: " + "#" + word + "Num" + " == " + word);
            //Debug.Log("Child: " + ReactorElement.Find<REtext.Selector>("#" + word + "Num")[0].textCmp.text + " == " + word);
            Assert.IsTrue(REtext.FindOne("#" + word + "Num").textCmp.text == word);
        }



        // Cahnge the list
        words.Add("Perro");
        words.Add("Gato");

        // Second check must not change
        Debug.Log("Brothers");
        Assert.IsTrue(REtext.FindOne("#" + words[0] + "Num").brothersSelector.Length == 5);

        Debug.Log("Childs");
        foreach (var word in words)
        {
            if (word == "Perro" || word == "Gato")
                continue;

            Debug.Log("Child: " + "#" + word + "Num" + " == " + word);
            Debug.Log("Child: " + REtext.FindOne("#" + word + "Num").textCmp.text + " == " + word);
            Assert.IsTrue(REtext.FindOne("#" + word + "Num").textCmp.text == word);
        }




        // Draw again the component
        Debug.Log("Drawing again");
        routerProve.Draw();
        // Check
        Debug.Log("Brothers: " + REtext.FindOne("#" + words[0] + "Num").brothersSelector.Length);
        Assert.IsTrue(REtext.FindOne("#" + words[0] + "Num").brothersSelector.Length == 7);

        Debug.Log("Childs");
        foreach (var word in words)
        {
            //Debug.Log("Child: " + "#" + word + "Num" + " == " + word);
            //Debug.Log("Child: " + ReactorElement.Find<REtext.Selector>("#" + word + "Num")[0].textCmp.text + " == " + word);
            Assert.IsTrue(REtext.FindOne("#" + word + "Num").textCmp.text == word);
        }






        // Cahnge the list
        words.Remove("Perro");
        words.Remove("Gato");

        // Draw again the component
        Debug.Log("Drawing again");
        routerProve.Draw();

        // Check
        Debug.Log("Brothers");
        Assert.IsTrue(REtext.FindOne("#" + words[0] + "Num").brothersSelector.Length == 5);

        Debug.Log("Childs");
        foreach (var word in words)
        {
            //Debug.Log("Child: " + "#" + word + "Num" + " == " + word);
            //Debug.Log("Child: " + ReactorElement.Find<REtext.Selector>("#" + word + "Num")[0].textCmp.text + " == " + word);
            Assert.IsTrue(REtext.FindOne("#" + word + "Num").textCmp.text == word);
        }




        // Cahnge the list
        words.Clear();

        // Draw again the component
        Debug.Log("Drawing again");
        routerProve.Draw();

        // Check
        Debug.Log("Brothers");
        Assert.IsTrue(REtext.FindOne("#1") == null);




        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }

    [UnityTest]
    public IEnumerator GenerateChildsAndChangeList()
    {
        var words = new List<string>
        {
            "One",
            "Two",
            "Three",
            "Four",
            "Five",
        };

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => words.Select(c =>
                    new REtext
                    {
                        propsReactorId = () => new REtext.ReactorIdSetter
                        {
                            id = c + "Num",
                        },
                        propsText = () => new REtext.TextSetter
                        {
                            text = c,
                        }
                    }
                ),

            };

        }
        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        Debug.Log("Drawing");
        routerProve.Draw();

        // First check
        Debug.Log("Brothers");
        Assert.IsTrue(REtext.FindOne("#" + words[0] + "Num").brothersSelector.Length == 5);

        foreach (var word in words)
        {
            //Debug.Log("Child: " + "#" + word + "Num" + " == " + word);
            //Debug.Log("Child: " + ReactorElement.Find<REtext.Selector>("#" + word + "Num")[0].textCmp.text + " == " + word);
            Assert.IsTrue(REtext.FindOne("#" + word + "Num").textCmp.text == word);
        }
        


        // Cahnge the list
        var wordsOld = words;
        words = new List<string>
        {
            "Six",
            "Seven",
            "Eight",
            "Nine",
        };

        // Second check and componenr must not change
        Debug.Log("Brothers: " + REtext.FindOne("#" + wordsOld[0] + "Num").brothersSelector.Length);
        Assert.IsTrue(REtext.FindOne("#" + wordsOld[0] + "Num").brothersSelector.Length == 5);

        foreach (var word in wordsOld)
        {
            if (word == "Perro" || word == "Gato")
                continue;

            Debug.Log("Child: " + "#" + word + "Num" + " == " + word);
            Debug.Log("Child: " + REtext.FindOne("#" + word + "Num").textCmp.text + " == " + word);
            Assert.IsTrue(REtext.FindOne("#" + word + "Num").textCmp.text == word);
        }




        // Draw again the component
        Debug.Log("Drawing again");
        routerProve.Draw(); Debug.Log("Drawing again");
        // Check
        Debug.Log("Brothers: " + REtext.FindOne("#" + words[0] + "Num")); 
        Debug.Log("Drawing again");
        Assert.IsTrue(REtext.FindOne("#" + words[0] + "Num").brothersSelector.Length == 4);

        Debug.Log("Childs");
        foreach (var word in words)
        {
            //Debug.Log("Child: " + "#" + word + "Num" + " == " + word);
            //Debug.Log("Child: " + ReactorElement.Find<REtext.Selector>("#" + word + "Num")[0].textCmp.text + " == " + word);
            Assert.IsTrue(REtext.FindOne("#" + word + "Num").textCmp.text == word);
        }





        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }


    class Level
    {
        public int number;
        public string name;
        public float time;
    }
    [UnityTest]
    public IEnumerator GenerateChildsFromArrayOfClasses()
    {


        var levels = new Level[]
        {
            new Level
            {
                number = 1,
                name = "LevelOne",
                time = 12f,
            },
            new Level
            {
                number = 2,
                name = "LevelTwo",
                time = 15.3f,
            },
            new Level
            {
                number = 3,
                name = "LevelThree",
                time = 12.12f,
            },
            new Level
            {
                number = 4,
                name = "LevelFour",
                time = 0f,
            },
        };

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => levels.Select(c =>
                    new REpanel
                    {
                        propsReactorId = () => new REpanel.ReactorIdSetter
                        {
                            id = c.number + "",
                        },
                        childs = () => new REbase[]
                        {
                            new REtext
                            {
                                propsText = () => new REtext.TextSetter
                                {
                                    text = c.name,
                                }
                            },
                            new REimage(),
                            new REtext
                            {
                                propsText = () => new REtext.TextSetter
                                {
                                    text = "Time: " + c.time,
                                }
                            }
                        }
                    }
                ),

            };

        }

        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        Debug.Log("Drawing");
        routerProve.Draw();

        Assert.IsTrue(REbase.FindOne("#" + levels[0].number + "").brothersSelector.Length == 4);

        Debug.Log("Childs");
        foreach (var level in levels)
        {
            //Debug.Log("Child: " + "#" + word + "Num" + " == " + word);
            //Debug.Log("Child: " + ReactorElement.Find<REtext.Selector>("#" + word + "Num")[0].textCmp.text + " == " + word);
            Assert.IsTrue(((REtext.Selector)REbase.FindOne("#" + level.number + "").childs[0]).textCmp.text == level.name);
            Assert.IsTrue(((REtext.Selector)REbase.FindOne("#" + level.number + "").childs[2]).textCmp.text == "Time: " + level.time);
        }

        routerProve.Erase();


    }




    [UnityTest]
    public IEnumerator Hooks_GenerateChildWithUseState()
    {

        var listState = new UseState<List<string>>(new List<string>());

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {
                propsReactorId = () => new REcanvas.ReactorIdSetter
                {
                    id = "MainCanvas",
                },
                useState = new IuseState[]
                {
                    listState,
                },
                childs = () => listState.value.Select(c =>
                    new REtext
                    {
                        propsReactorId = () => new REtext.ReactorIdSetter
                        {
                            id = c + "Num",
                        },
                        propsText = () => new REtext.TextSetter
                        {
                            text = c,
                        }
                    }
                ),

            };

        }

        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        Debug.Log("Drawing");
        routerProve.Draw();

        // First check
        Debug.Log("Childs: " + REbase.FindOne("#MainCanvas").childs.Length);
        Assert.IsTrue(REbase.FindOne("#MainCanvas").childs.Length == 0);



        // Simulate the fetch
        yield return new WaitForSecondsRealtime(1);
        var fetchData = new List<string>
        {
            "Six",
            "Seven",
            "Eight",
            "Nine",
        };




        // Draw again the component
        Debug.Log("UseState: " + listState.value.Count());
        listState.SetState(fetchData);

        // Check
        Debug.Log("Childs: " + REbase.FindOne("#MainCanvas").childs.Length);
        Assert.IsTrue(REbase.FindOne("#MainCanvas").childs.Length == 4);

        Debug.Log("Childs");
        foreach (var word in listState.value)
        {
            //Debug.Log("Child: " + "#" + word + "Num" + " == " + word);
            //Debug.Log("Child: " + ReactorElement.Find<REtext.Selector>("#" + word + "Num")[0].textCmp.text + " == " + word);
            Assert.IsTrue(REtext.FindOne("#" + word + "Num").textCmp.text == word);
        }





        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();



    }

    [UnityTest]
    public IEnumerator Hooks_GenerateChildWithUseState_NotInitializatingTheHook()
    {

        var listState = new UseState<List<string>>(); // <-- Not create a new list

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {
                propsReactorId = () => new REcanvas.ReactorIdSetter
                {
                    id = "MainCanvas",
                },
                useState = new IuseState[]
                {
                    listState,
                },
                childs = () => listState.value.Select(c =>
                    new REtext
                    {
                        propsReactorId = () => new REtext.ReactorIdSetter
                        {
                            id = c + "Num",
                        },
                        propsText = () => new REtext.TextSetter
                        {
                            text = c,
                        }
                    }
                ),

            };

        }

        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        Debug.Log("Drawing");
        routerProve.Draw();


        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();

    }


    [UnityTest]
    public IEnumerator Hooks_GenerateChildWith_ButThrowError()
    {

        var listState = new UseState<List<string>>(); // <-- Not create a new list

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {
                propsReactorId = () => new REcanvas.ReactorIdSetter
                {
                    id = "MainCanvas",
                },
                useState = new IuseState[]
                {
                    listState,
                },
                childs = () => throw new Exception("Expected exception"),

            };

        }

        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        Debug.Log("Drawing");
        Assert.Throws<Exception>(() => routerProve.Draw());

        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();

    }




    [UnityTest]
    public IEnumerator Hooks_GenerateChildWithUseState_ManualTriggerToChange()
    {

        var listState = new UseState<List<string>>(new List<string>());

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {
                propsReactorId = () => new REcanvas.ReactorIdSetter
                {
                    id = "MainCanvas",
                },
                useState = new IuseState[]
                {
                    listState,
                },
                childs = () => listState.value.Select(c =>
                    new REtext
                    {
                        propsReactorId = () => new REtext.ReactorIdSetter
                        {
                            id = c + "Num",
                        },
                        propsText = () => new REtext.TextSetter
                        {
                            text = c,
                        }
                    }
                ),

            };

        }

        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        Debug.Log("Drawing");
        routerProve.Draw();

        // First check
        Debug.Log("Childs: " + REbase.FindOne("#MainCanvas").childs.Length);
        Assert.IsTrue(REbase.FindOne("#MainCanvas").childs.Length == 0);







        // usestate the component adding elements to the list manually
        Debug.Log("UseState: " + listState.value.Count());
        listState.value.Add("Dog");
        listState.value.Add("Cat");

        // Check, childs are 0, because the useState will not trigger if .SetState is not used
        Debug.Log("Childs: " + REbase.FindOne("#MainCanvas").childs.Length);
        Assert.IsTrue(REbase.FindOne("#MainCanvas").childs.Length == 0);



        // But can be triggered manually
        listState.SetState();
        // Check, childs are 2, because the useState will not trigger if .SetState is not used
        Debug.Log("Childs: " + REbase.FindOne("#MainCanvas").childs.Length);
        Assert.IsTrue(REbase.FindOne("#MainCanvas").childs.Length == 2);



        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();



    }


}