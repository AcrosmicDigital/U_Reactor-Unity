using UnityEditor;
using static U.Reactor.Editor.UE;

namespace U.Reactor.Editor
{
    public class CreateEnvIdsAndClassnamesFileMenuButton : EditorWindow
    {

        #region File
        private static string FolderName => "/Scripts/Env/";
        private static string FileName => "ClassNames.env.cs";
        private readonly static string[] file =
        {
            "",
            "public static partial class Env",
            "{",
            "    public static partial class ClassNames",
            "    {",
            "        // Examples, you can delete them",
            "        //public static string Name => " + quote + "Name" + quote + ";",
            "        //public static string Level(int num) => "+quote+"Level"+quote+" + num;",
            "",
            "        // ... Add here your classNames",
            "",
            "",
            "    }",
            "}",
        };
        #endregion File



        private static string FormatLog(string text) => "Reactor: " + text;


        [MenuItem("Universal/Reactor/Create/Env Classnames")]
        public static void ShowWindow()
        {

            // Create files
            CreateFile(FolderName, FileName, file, FormatLog);

            // Compile
            AssetDatabase.Refresh();

        }

    }
}