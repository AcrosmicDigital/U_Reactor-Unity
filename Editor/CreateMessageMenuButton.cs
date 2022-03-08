using UnityEditor;
using static U.Reactor.Editor.UE;

namespace U.Reactor.Editor
{
    public class CreateMessageMenuButton : EditorWindow
    {

        #region File
        private static string DefaultFolderName => "/Scripts/Reactor/Messages/";
        private static string DefaultFileName => "NewMessage";
        private static string CustomExtension => "message";
        static string[] File(string fileName) => new string[]
        {
            "using System;",
            "using U.Reactor;",
            "using UnityEngine;",
            "",
            "public static partial class RRmessages",
            "{",
            "",
            "    public static void "+fileName+"()",
            "    {",
            "",
            "        // Load Resources",
            "        //UIIcons rscIcons = Resources.Load<UIIcons>("+quote+"UIIcons"+quote+");",
            "        //...",
            "",
            "",
            "        new REcanvas",
            "        {",
            "            propsGameObject = () => new REcanvas.GameObjectSetter",
            "            {",
            "                name = "+quote+""+fileName+"-Message"+quote+"",
            "            },",
            "            propsCanvasScaler = () => new REcanvas.CanvasScalerSetter",
            "            {",
            "                matchWidthOrHeight = 1,",
            "            },",
            "            propsCanvas = () => new REcanvas.CanvasSetter",
            "            {",
            "                sortingOrder = 1,",
            "            },",
            "            childs = () => new REbase[]",
            "            {",
            "                new REpanel",
            "                {",
            "                    propsImage = () => new REpanel.ImageSetter",
            "                    {",
            "                        color = new Color(0,0,0,0.4f),",
            "                    },",
            "                    childs = () => new REbase[]",
            "                    {",
            "                        new REimage",
            "                        {",
            "                            propsImage = () => new REimage.ImageSetter",
            "                            {",
            "                                color = new Color(0.2358491f,0.2358491f,0.2358491f),",
            "                            },",
            "                            propsRectTransform = () => new REimage.RectTransformSetter {",
            "                                height = 250,",
            "                                width = 440,",
            "                            },",
            "                            childs = () => new REbase[]",
            "                            {",
            "                                new REtext",
            "                                {",
            "                                    propsRectTransform = () => new REtext.RectTransformSetter",
            "                                    {",
            "                                        localPosition = new Vector2(0, 42),",
            "                                    },",
            "                                    propsText = () => new REtext.TextSetter",
            "                                    {",
            "                                        text = "+quote+""+fileName+""+quote+",",
            "                                        alignment = TextAnchor.MiddleCenter,",
            "                                        fontColor = Color.white,",
            "                                    }",
            "                                },",
            "                                new REbutton",
            "                                {",
            "                                    propsRectTransform = () => new REbutton.RectTransformSetter",
            "                                    {",
            "                                        width = 200,",
            "                                        height = 50,",
            "                                        anchorMin = new Vector2(0.5f, 0f),",
            "                                        anchorMax = new Vector2(0.5f, 0f),",
            "                                        localPosition = new Vector2(0, 75),",
            "                                    },",
            "                                    propsText = () => new REbutton.TextSetter",
            "                                    {",
            "                                        text = "+quote+"Close"+quote+",",
            "                                    },",
            "                                    propsImage = () => new REbutton.ImageSetter",
            "                                    {",
            "                                        color = new Color(0.8679245f,0.8679245f,0.8679245f),",
            "                                    },",
            "                                    propsButton = () => new REbutton.ButtonSetter",
            "                                    {",
            "                                        OnClickListener = (s) =>",
            "                                        {",
            "                                            s.rootCanvasSelector.Erase();",
            "                                        },",
            "                                    },",
            "                                },",
            "                            },",
            "                        },",
            "                    },",
            "                },",
            "            },",
            "        }.Draw().ToDontDestroyOnLoad();",
            "    }",
            "",
            "}",
        };
        #endregion File



        private static string FormatLog(string text) => "Reactor: " + text;


        [MenuItem("Universal/Reactor/Create/Message")]
        public static void ShowWindow()
        {

            // Create files
            CreateFileWithSaveFilePanelAndCustomExtension(DefaultFolderName, DefaultFileName, File, FormatLog, CustomExtension);

            // Compile
            AssetDatabase.Refresh();

        }

    }
}