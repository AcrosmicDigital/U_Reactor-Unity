using UnityEditor;
using static U.Reactor.Editor.UE;

namespace U.Reactor.Editor
{
    public class CreateGameControllerMenuButton : EditorWindow
    {

        #region File
        private static string FolderName => "/Scripts/Controllers/";
        private static string FileName => "UI.controller.cs";
        private readonly static string[] file =
        {
            "using System;",
            "using UnityEngine;",
            "",
            "public static partial class Control",
            "{",
            "    public static partial class UI",
            "    {",
            "",
            "        public static void RenderHome()",
            "        {",
            "            Debug.Log("+quote+"UIController: RenderHome"+quote+");",
            "            //RRouting.MainRouter.Route(RRouting.Routes.Home);",
            "            // ...",
            "        }",
            "",
            "        public static void EraseAll()",
            "        {",
            "            Debug.Log("+quote+"UIController: EraseAll"+quote+");",
            "            //RRouting.MainRouter.Erase();",
            "            // ...",
            "        }",
            "",
            "    }",
            "}",
        };
        #endregion File



        private static string FormatLog(string text) => "Reactor: " + text;


        [MenuItem("Universal/Reactor/Create/UI Controller")]
        public static void ShowWindow()
        {

            // Create files
            CreateFile(FolderName, FileName, file, FormatLog);

            // Compile
            AssetDatabase.Refresh();

        }

    }
}