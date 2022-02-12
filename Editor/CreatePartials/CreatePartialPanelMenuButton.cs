using UnityEditor;
using static U.Reactor.Editor.UE;

namespace U.Reactor.Editor
{
    public class CreatePartialPanelMenuButton : EditorWindow
    {

        #region File
        private static string DefaultFolderName => "/Scripts/Reactor/Partials/";
        private static string DefaultFileName => "New";
        private static string CustomExtension => "panel.partial";
        static string[] File(string fileName) => new string[]
        {
            "using UnityEngine;",
            "using System;",
            "using U.Reactor;",
            "using System.Collections.Generic;",
            "",
            "public static partial class RRpartials",
            "{",
            "    public static partial class Panels",
            "    {",
            "        public static REpanel "+fileName+"(",
            "            Func<REpanel.RectTransformSetter> propsRectTransform,",
            "            Func<IEnumerable<REbase>> childs",
            "            )",
            "        {",
            "",
            "            // Load Resources",
            "            //UIButtons rscButtons = Resources.Load<UIButtons>("+quote+"UIButtons"+quote+");",
            "            //...",
            "",
            "            return new REpanel",
            "            {",
            "                propsRectTransform = propsRectTransform,",
            "            };",
            "        }",
            "    }",
            "}",
        };
        #endregion File



        private static string FormatLog(string text) => "Reactor: " + text;


        [MenuItem("Universal/Reactor/Create/Partial/Panel")]
        public static void ShowWindow()
        {

            // Create files
            CreateFileWithSaveFilePanelAndCustomExtension(DefaultFolderName, DefaultFileName, File, FormatLog, CustomExtension);

            // Compile
            AssetDatabase.Refresh();

        }

    }
}