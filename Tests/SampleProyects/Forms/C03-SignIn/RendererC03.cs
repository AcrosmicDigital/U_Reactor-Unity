using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U.Reactor;
using System.Linq;

public class RendererC03 : MonoBehaviour
{
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }


        public static User Find(string username, string password)
        {
            return database.Where(u => u.username == username && u.password == password).FirstOrDefault();
        }

        public static User[] Find()
        {
            return database.ToArray();
        }

        public bool Save()
        {
            if (username.Length < 2) return false;

            database.Add(this);

            return true;
        }

    }

    static List<User> database = new List<User>
    {
        new User
        {
            username = "Robert01",
            password = "TheOne"
        },
        new User
        {
            username = "Ema",
            password = "12December"
        },
        new User
        {
            username = "TheWildWolf",
            password = "password"
        },
    };



    // Start is called before the first frame update
    void Start()
    {
        // Render component
        SingnInView().Draw();
    }


    private REcanvas SingnInView()
    {
        string usernameText = "";
        string passwordText = "";
        User currentUser = null;
        string error = "Write your credentials";
        bool rememberme = false;

        // Hooks
        var viweState = new UseState<int>(0);
        var errorTrigger = new UseTrigger();


        return new REcanvas
        {
            childs = () =>
            {
                // Profile
                if(viweState.value == 1)
                {
                    return new REbase[]
                    {
                        new REpanelVertical
                        {
                            propsRectTransform = () => REpanelVertical.TableRectTransform(20,80,20,80),
                            propsVerticalLayoutGroup = () => new REpanelVertical.VerticalLayoutGroupSetter{
                                childAlignment = TextAnchor.MiddleCenter,
                            },
                            childs = () => new REbase[]
                            {
                                new REtext
                                {
                                    propsText = () => new REtext.TextSetter
                                    {
                                        text = "Welcome " + currentUser.username,
                                    }
                                },
                                new REtext
                                {
                                    propsText = () => new REtext.TextSetter
                                    {
                                        text = "Username: " + currentUser.username,
                                    }
                                },
                                new REtext
                                {
                                    propsText = () => new REtext.TextSetter
                                    {
                                        text = "Password: " + currentUser.password,
                                    }
                                },
                                new REbutton
                                {
                                    propsText = () => new REbutton.TextSetter
                                    {
                                        text = "Logout"
                                    },
                                    propsButton = () => new REbutton.ButtonSetter
                                    {
                                        OnClickListener = (s) =>
                                        {
                                            currentUser = null;

                                            error = "Write your credentials";
                                            if (!rememberme)
                                            {
                                                usernameText = "";
                                                passwordText = "";
                                            }
                                            errorTrigger.Trigger();
                                            viweState.SetState(0);
                                        }
                                    }
                                },
                                new REtext
                                {
                                    propsText = () => new REtext.TextSetter
                                    {
                                        text = error,
                                    },
                                    useTrigger = new UseTrigger.Hook[]
                                    {
                                        new UseTrigger.Hook
                                        {
                                            hook = errorTrigger,
                                            OnTrigger = (s) =>
                                            {
                                                REtext.CastSelector(s).textCmp.text = error;
                                            }
                                        }
                                    }
                                },
                                new REbox
                                {
                                    propsRectTransform = () => new REbox.RectTransformSetter
                                    {
                                        height = 400,
                                        width = 800,
                                    },
                                    childs = () => new REbase[]
                                    {
                                        new REpanelVertical
                                        {
                                            childs = () => User.Find().Select(u => new REtext
                                                {
                                                    propsText = () => new REtext.TextSetter
                                                    {
                                                        text = u.username,
                                                    }
                                                }
                                            ),
                                        }
                                    }
                                }
                            }
                        },
                    };
                }

                // Sign Up Component
                else if(viweState.value == 3)
                {
                    string newUsername = "";
                    string newPassword = "";

                    return new REbase[]
                    {
                        new REpanelVertical
                        {
                            propsRectTransform = () => REpanelVertical.TableRectTransform(20,80,20,80),
                            propsVerticalLayoutGroup = () => new REpanelVertical.VerticalLayoutGroupSetter{
                                childAlignment = TextAnchor.MiddleCenter,
                            },
                            childs = () => new REbase[]
                            {
                                new REtext
                                {
                                    propsText = () => new REtext.TextSetter
                                    {
                                        text = "Sign Up"
                                    }
                                },
                                new REtext
                                {
                                    propsText = () => new REtext.TextSetter
                                    {
                                        text = "Username"
                                    }
                                },
                                new REinputField
                                {
                                    propsInputField = () => new REinputField.InputFieldSetter
                                    {
                                        text = newUsername,
                                        OnValueChangedListener = (v,s) =>
                                        {
                                            newUsername = v;
                                        }
                                    }
                                },
                                new REtext
                                {
                                    propsText = () => new REtext.TextSetter
                                    {
                                        text = "Password"
                                    }
                                },
                                new REinputField
                                {
                                    propsInputField = () => new REinputField.InputFieldSetter
                                    {
                                        text = newPassword,
                                        OnValueChangedListener = (v,s) =>
                                        {
                                            newPassword = v;
                                        }
                                    }
                                },
                                new REbutton
                                {
                                    propsText = () => new REbutton.TextSetter
                                    {
                                        text = "Create"
                                    },
                                    propsButton = () => new REbutton.ButtonSetter
                                    {
                                        OnClickListener = (s) =>
                                        {
                                            var newUser = new User
                                            {
                                                username = newUsername,
                                                password = newPassword,
                                            }.Save();

                                            if(newUser)
                                            {
                                                error = "Succesfull Created";
                                                newUsername = "";
                                                newPassword = "";
                                                errorTrigger.Trigger();
                                                viweState.SetState(0);
                                            }
                                            else
                                            {
                                                error = "Cant create a user";
                                                errorTrigger.Trigger();
                                            }
                                        }
                                    }
                                },
                                new REtext
                                {
                                    propsText = () => new REtext.TextSetter
                                    {
                                        text = error,
                                    },
                                    useTrigger = new UseTrigger.Hook[]
                                    {
                                        new UseTrigger.Hook
                                        {
                                            hook = errorTrigger,
                                            OnTrigger = (s) =>
                                            {
                                                REtext.CastSelector(s).textCmp.text = error;
                                            }
                                        }
                                    }
                                },
                                new REbutton
                                {
                                    propsText = () => new REbutton.TextSetter
                                    {
                                        text = "Sign In"
                                    },
                                    propsButton = () => new REbutton.ButtonSetter
                                    {
                                        OnClickListener = (s) =>
                                        {
                                            error = "Write your credentials";
                                            errorTrigger.Trigger();
                                            viweState.SetState(0);
                                        }
                                    }
                                },
                            }
                        },
                    };
                }

                // Sign In Component
                else
                {
                    return new REbase[]
                    {
                        new REpanelVertical
                        {
                            propsRectTransform = () => REpanelVertical.TableRectTransform(20,80,20,80),
                            propsVerticalLayoutGroup = () => new REpanelVertical.VerticalLayoutGroupSetter{
                                childAlignment = TextAnchor.MiddleCenter,
                            },
                            childs = () => new REbase[]
                            {
                                new REtext
                                {
                                    propsText = () => new REtext.TextSetter
                                    {
                                        text = "Sign In"
                                    }
                                },
                                new REtext
                                {
                                    propsText = () => new REtext.TextSetter
                                    {
                                        text = "Username"
                                    }
                                },
                                new REinputField
                                {
                                    propsInputField = () => new REinputField.InputFieldSetter
                                    {
                                        text = usernameText,
                                        OnValueChangedListener = (v,s) =>
                                        {
                                            usernameText = v;
                                        }
                                    }
                                },
                                new REtext
                                {
                                    propsText = () => new REtext.TextSetter
                                    {
                                        text = "Password"
                                    }
                                },
                                new REinputField
                                {
                                    propsInputField = () => new REinputField.InputFieldSetter
                                    {
                                        text = passwordText,
                                        OnValueChangedListener = (v,s) =>
                                        {
                                            passwordText = v;
                                        }
                                    }
                                },
                                new REtoggle
                                {
                                    propsText = () => new REtoggle.TextSetter
                                    {
                                        text = "Remember me"
                                    },
                                    propsToggle = () => new REtoggle.ToggleSetter
                                    {
                                        isOn = rememberme,
                                        OnValueChangedListener = (v,s) => rememberme = v,
                                    }
                                },
                                new REbutton
                                {
                                    propsText = () => new REbutton.TextSetter
                                    {
                                        text = "Login"
                                    },
                                    propsButton = () => new REbutton.ButtonSetter
                                    {
                                        OnClickListener = (s) =>
                                        {
                                            var findUser = User.Find(usernameText, passwordText);
                                            currentUser = findUser;

                                            if(findUser != null)
                                            {
                                                Debug.Log("Username: " + findUser.username + " Password: " + findUser.password);
                                                error = "Succesfull sign In";
                                                errorTrigger.Trigger();
                                                viweState.SetState(1);
                                            }
                                            else
                                            {
                                                error = "Invalid credentials, try again";
                                                errorTrigger.Trigger();
                                            }
                                        }
                                    }
                                },
                                new REtext
                                {
                                    propsText = () => new REtext.TextSetter
                                    {
                                        text = error,
                                    },
                                    useTrigger = new UseTrigger.Hook[]
                                    {
                                        new UseTrigger.Hook
                                        {
                                            hook = errorTrigger,
                                            OnTrigger = (s) =>
                                            {
                                                REtext.CastSelector(s).textCmp.text = error;
                                            }
                                        }
                                    }
                                },
                                new REbutton
                                {
                                    propsText = () => new REbutton.TextSetter
                                    {
                                        text = "Sign Up"
                                    },
                                    propsButton = () => new REbutton.ButtonSetter
                                    {
                                        OnClickListener = (s) =>
                                        {
                                            error = "Create your credentials";
                                            errorTrigger.Trigger();
                                            viweState.SetState(3);
                                        }
                                    }
                                },
                            }
                        },
                    };
                }

                

            },
            useState = new IuseState[]
            {
                viweState,
            }
        };
    }

}
