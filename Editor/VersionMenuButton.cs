using UnityEngine;
using UnityEditor;

namespace U.Reactor.Editor
{
    internal class VersionMenuButton : EditorWindow
    {

        [MenuItem("Universal/Reactor/Version")]
        public static void PrintVersion()
        {

            Debug.Log(" U Framework: Reactor v1.0.0 for Unity");

        }
    }
}