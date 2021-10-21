using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;
using U.Reactor;

namespace U.Reactor
{
    public static partial class HC
    {
        public class MultiToggle : MonoBehaviour
        {

            public int maxEnabled = 0;  // Max enaled, 0 or less is all ca be enabled
            public ToggleDef[] toggleDefs;
            public UnityEvent<ToggleSet[]> OnValueChanged = new UnityEvent<ToggleSet[]>();


            private ToggleSet[] lastState; // Last set of toggle ative

            // Validate maxEnabled value
            private int _maxEnabled
            {
                get
                {
                    if (maxEnabled < 1 || maxEnabled > toggleDefs.Length)
                        return toggleDefs.Length;
                    else
                        return maxEnabled;

                }
            }



            private void Start()
            {
                if (toggleDefs == null)
                {
                    //Debug.LogError("MultiToggle: On gameobject " + gameObject.name + ", toggle Defs are not defined ");
                    return;
                }

                if (toggleDefs.Length < 1)
                {
                    //Debug.LogError("MultiToggle: On gameobject " + gameObject.name + ", toggle Defs are not defined ");
                    return;
                }

                int enabledCount = 0;

                for (int i = 0; i < toggleDefs.Length; i++)
                {
                    if (toggleDefs[i].toggle == null)
                    {
                        Debug.LogError("MultiToggle: On gameobject " + gameObject.name + ", toggle component cant be null ");
                        return;
                    }

                    // Count and disable if necesary
                    if (toggleDefs[i].toggle.isOn)
                        enabledCount++;

                    if (enabledCount > _maxEnabled)
                        toggleDefs[i].toggle.isOn = false;


                    toggleDefs[i].toggle?.onValueChanged.AddListener(OnToggleChage);


                    // Set the actual state of the toogle as last state
                    toggleDefs[i].currentState = toggleDefs[i].toggle.isOn;

                }

                // Get last state
                lastState = toggleDefs.Where(t => t.toggle.isOn).Select(t => new ToggleSet { name = t.name, number = t.number, value = t.value }).ToArray();


                //// Subscibe to interna event to prove
                //OnValueChanged.AddListener(t => 
                //{
                //    Debug.Log("L " + t.Length);
                //    foreach (var toggle in t)
                //    {
                //        Debug.Log("N " + toggle.name + " I: " + toggle.number + " V: " + toggle.value);
                //    }
                //});
            }


            private void OnToggleChage(bool active)
            {

                // Get the changed
                var chanched = toggleDefs.Where(t => t.currentState != t.toggle.isOn).FirstOrDefault();

                // Count enaled
                var enabledCount = toggleDefs.Where(t => t.toggle.isOn).Count();

                // Check if can be enabled or disable it
                if (enabledCount > _maxEnabled)
                {
                    chanched.toggle.isOn = false;

                    // Return and dont invoke event
                    return;
                }

                //Debug.Log("Changed N " + chanched.name + " I: " + chanched.number + " V: " + chanched.value + " S: " + chanched.toggle.isOn);

                // Update to new states
                for (int i = 0; i < toggleDefs.Length; i++)
                {
                    // Set the actual state of the toogle as last state
                    toggleDefs[i].currentState = toggleDefs[i].toggle.isOn;

                }

                // Get the enabled toggles
                var newState = toggleDefs.Where(t => t.toggle.isOn).Select(t => new ToggleSet { name = t.name, number = t.number, value = t.value }).ToArray();


                //foreach (var item in lastState)
                //{
                //    Debug.Log("Last N " + item.name + " I: " + item.number + " V: " + item.value + " S: ");
                //}

                //foreach (var item in newState)
                //{
                //    Debug.Log("New N " + item.name + " I: " + item.number + " V: " + item.value + " S: ");
                //}

                //Debug.Log("Equals" + (newState[0] == lastState[0]));
                //Debug.Log("Equals" + (newState[1] == lastState[1]));
                //Debug.Log("Equals" + (newState[0] == lastState[1]));

                // Enable the event with the changed toggle
                if (!newState.SequenceEqual(lastState))
                {
                    try
                    {
                        OnValueChanged?.Invoke(newState);
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError("MultiToggle: On gameobject " + gameObject.name + ", Exception ivoking event OnValueChanged, " + e);
                    }
                }


                // New state is now last state
                lastState = newState;
            }



            [System.Serializable]
            public class ToggleDef
            {
                public Toggle toggle;
                public Text label;
                public string name;
                public int number;
                public float value;
                public bool currentState { get; set; }
            }


            [System.Serializable]
            public class ToggleSet
            {
                public string name;
                public int number;
                public float value;

                public override bool Equals(object obj)
                {
                    if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                    {
                        return false;
                    }
                    else
                    {

                        ToggleSet t = (ToggleSet)obj;

                        return name == t.name && number == t.number && value == t.value;

                    }
                }

                public static bool operator ==(ToggleSet t1, ToggleSet t2)
                {
                    var equal = true;

                    try
                    {
                        equal = t1.Equals(t2);
                    }
                    catch (System.Exception)
                    {
                        try
                        {
                            equal = t2.Equals(t1);
                        }
                        catch (System.Exception)
                        {

                        }
                    }

                    return equal;
                }

                public static bool operator !=(ToggleSet t1, ToggleSet t2)
                {
                    return !(t1 == t2);
                }

                public override int GetHashCode()
                {
                    return name.GetHashCode() + number.GetHashCode() + value.GetHashCode();
                }

            }



        }
    }
}