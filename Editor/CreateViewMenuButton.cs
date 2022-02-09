using UnityEditor;
using static U.Reactor.Editor.UE;

namespace U.Reactor.Editor
{
    public class CreateViewMenuButton : EditorWindow
    {

        #region File
        private static string DefaultFolderName => "/Scripts/Reactor/Views/";
        private static string DefaultFileName => "NewView";
        static string[] RenderFile(string fileName) => new string[]
        {
            "using UnityEngine;",
            "using U.Reactor;",
            "",
            "public static partial class RViews",
            "{",
            "    // To draw a instance of this View use: new RViews."+fileName+"().Create().Draw();",
            "    public partial class "+fileName+"",
            "    {",
            "        public REcanvas Create()",
            "        {",
            "            return new REcanvas",
            "            {",
            "                propsCanvasScaler = () => new REcanvas.CanvasScalerSetter",
            "                {",
            "                    matchWidthOrHeight = 1,",
            "                },",
            "                propsGameObject = () => new REcanvas.GameObjectSetter",
            "                {",
            "                    name = "+quote+""+fileName+" View"+quote+",",
            "                },",
            "                childs = () => new REbase[]",
            "                {",
            "                    new RElabel",
            "                    {",
            "                        propsRectTransform = () => new RElabel.RectTransformSetter",
            "                        {",
            "                            localPosition = new Vector3(0, 84, 0),",
            "                        },",
            "                        propsText = () => new RElabel.TextSetter",
            "                        {",
            "                            text = "+quote+""+fileName+" View"+quote+"",
            "                        },",
            "                        propsImage = () => new RElabel.BackImageSetter",
            "                        {",
            "                            color = new Color(0.7264151f, 0.7264151f, 0.7264151f, 1),",
            "                        }",
            "                    },",
            "                    new REbutton",
            "                    {",
            "                        propsButton = () => new REbutton.ButtonSetter",
            "                        {",
            "                            OnClickListener = ButtonClickListener,",
            "                        }",
            "                    }",
            "                }",
            "            };",
            "        }",
            "    }",
            "}",
        };
        static string[] ScriptsFile(string fileName) => new string[]
        {
            "using U.Reactor;",
            "using UnityEngine;",
            "",
            "public static partial class RViews",
            "{",
            "    public partial class "+fileName+"",
            "    {",
            "",
            "        // Load Resources",
            "        //UIButtons spritesButtons = Resources.Load<UIButtons>("+quote+"Buttons"+quote+");",
            "        //...",
            "",
            "",
            "        public void ButtonClickListener(REbutton.Selector s)",
            "        {",
            "            Debug.Log("+quote+"Clicked: "+quote+" + s.rootCanvasSelector.gameObject.name + "+quote+" Button"+quote+");",
            "            // ...",
            "        }",
            "",
            "    }",
            "}",
        };
        #endregion File



        private static string FormatLog(string text) => "Reactor: " + text;


        [MenuItem("Universal/Reactor/Create/View")]
        public static void ShowWindow()
        {

            // Create files
            CreateViewFiles(DefaultFolderName, DefaultFileName, RenderFile, ScriptsFile, FormatLog);

            // Compile
            AssetDatabase.Refresh();

        }

    }
}