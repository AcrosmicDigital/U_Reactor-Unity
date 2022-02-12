using UnityEditor;
using static U.Reactor.Editor.UE;

namespace U.Reactor.Editor
{
    public class CreatePartialSliderMenuButton : EditorWindow
    {

        #region File
        private static string DefaultFolderName => "/Scripts/Reactor/Partials/";
        private static string DefaultFileName => "New";
        private static string CustomExtension => "slider.partial";
        static string[] File(string fileName) => new string[]
        {
            "using UnityEngine;",
            "using System;",
            "using U.Reactor;",
            "",
            "public static partial class RRpartials",
            "{",
            "    public static partial class Sliders",
            "    {",
            "        public static REslider "+fileName+"(",
            "            Func<REslider.RectTransformSetter> propsRectTransform",
            "            )",
            "        {",
            "",
            "            // Load Resources",
            "            //UIButtons rscButtons = Resources.Load<UIButtons>("+quote+"UIButtons"+quote+");",
            "            //...",
            "",
            "            return new REslider",
            "            {",
            "                propsRectTransform = propsRectTransform,",
            "            };",
            "        }",
            "    }",
            "}",
        };
        #endregion File



        private static string FormatLog(string text) => "Reactor: " + text;


        [MenuItem("Universal/Reactor/Create/Partial/Slider")]
        public static void ShowWindow()
        {

            // Create files
            CreateFileWithSaveFilePanelAndCustomExtension(DefaultFolderName, DefaultFileName, File, FormatLog, CustomExtension);

            // Compile
            AssetDatabase.Refresh();

        }

    }
}