using UnityEngine;
using UnityEditor;


#if UNITY_EDITOR

public class VersionMenuButton : EditorWindow
{

    [MenuItem("U/Reactor/Version")]
    public static void PrintVersion()
    {

        Debug.Log(" U Framework: Reactor v1.0.0 for Unity");

    }
}


#endif
