using UnityEditor;
using static U.Reactor.Editor.UE;

namespace U.Reactor.Editor
{
    public class CreatePartialTextMenuButton : EditorWindow
    {

        #region File
        private static string DefaultFolderName => "/Scripts/Reactor/Partials/";
        private static string DefaultFileName => "New";
        private static string CustomExtension => "text.partial";
        static string[] File(string fileName) => new string[]
        {
            "using UnityEngine;",
            "using System;",
            "using U.Reactor;",
            "",
            "public static partial class RRpartials",
            "{",
            "    public static partial class Texts",
            "    {",
            "        public static REtext "+fileName+"(",
            "            Func<REtext.RectTransformSetter> propsRectTransform",
            "            )",
            "        {",
            "",
            "            // Load Resources",
            "            //UIButtons rscButtons = Resources.Load<UIButtons>("+quote+"UIButtons"+quote+");",
            "            //...",
            "",
            "            return new REtext",
            "            {",
            "                propsRectTransform = propsRectTransform,",
            "            };",
            "        }",
            "    }",
            "}",
        };
        #endregion File



        private static string FormatLog(string text) => "Reactor: " + text;


        [MenuItem("Universal/Reactor/Create/Partial/Text")]
        public static void ShowWindow()
        {

            // Create files
            CreateFileWithSaveFilePanelAndCustomExtension(DefaultFolderName, DefaultFileName, File, FormatLog, CustomExtension);

            // Compile
            AssetDatabase.Refresh();

        }

    }
}