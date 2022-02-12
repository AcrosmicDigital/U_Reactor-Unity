using UnityEditor;
using static U.Reactor.Editor.UE;

namespace U.Reactor.Editor
{
    public class CreatePartialButtonMenuButton : EditorWindow
    {

        #region File
        private static string DefaultFolderName => "/Scripts/Reactor/Partials/";
        private static string DefaultFileName => "New";
        private static string CustomExtension => "button.partial";
        static string[] File(string fileName) => new string[]
        {
            "using UnityEngine;",
            "using System;",
            "using U.Reactor;",
            "",
            "public static partial class RRpartials",
            "{",
            "    public static partial class Buttons",
            "    {",
            "        public static REbutton "+fileName+"(",
            "            Func<REbutton.RectTransformSetter> propsRectTransform,",
            "            Sprite sprite,",
            "            bool interactable,",
            "            Action<REbutton.Selector> onClickListener",
            "            )",
            "        {",
            "",
            "            // Load Resources",
            "            //UIButtons rscButtons = Resources.Load<UIButtons>("+quote+"UIButtons"+quote+");",
            "            //...",
            "",
            "            return new REbutton",
            "            {",
            "                propsRectTransform = propsRectTransform,",
            "                propsText = () => new REbutton.TextSetter",
            "                {",
            "                    text = "+quote+""+quote+"",
            "                },",
            "                propsImage = () => new REbutton.ImageSetter",
            "                {",
            "                    sprite = sprite,",
            "                },",
            "                propsButton = () => new REbutton.ButtonSetter",
            "                {",
            "                    interactable = interactable,",
            "                    OnClickListener = onClickListener,",
            "                },",
            "            };",
            "        }",
            "    }",
            "}",
        };
        #endregion File



        private static string FormatLog(string text) => "Reactor: " + text;


        [MenuItem("Universal/Reactor/Create/Partial/Button")]
        public static void ShowWindow()
        {

            // Create files
            CreateFileWithSaveFilePanelAndCustomExtension(DefaultFolderName, DefaultFileName, File, FormatLog, CustomExtension);

            // Compile
            AssetDatabase.Refresh();

        }

    }
}