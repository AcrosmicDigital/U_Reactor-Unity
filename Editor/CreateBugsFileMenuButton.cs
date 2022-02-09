using UnityEditor;
using static U.Gears.Editor.UE;

namespace U.Gears.Editor
{
    public class CreateBugsFileMenuButton : EditorWindow
    {

        #region File
        private static string FolderName => "/Extras/";
        private static string FileName => "Bugs.desc.txt";
        private readonly static string[] file =
        {
            "",
            "Bugs",
            "",
            "- Bug1: Description",
        };
        #endregion File



        private static string FormatLog(string text) => "UniversalGears: " + text;


        [MenuItem("Universal/Gears/Create/Bugs Desc")]
        public static void ShowWindow()
        {

            // Create files
            CreateFile(FolderName, FileName, file, FormatLog);

            // Compile
            AssetDatabase.Refresh();

        }

    }
}