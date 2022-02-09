using UnityEditor;
using static U.Gears.Editor.UE;

namespace U.Gears.Editor
{
    public class CreateMissingFileMenuButton : EditorWindow
    {

        #region File
        private static string FolderName => "/Extras/";
        private static string FileName => "Missing.desc.txt";
        private readonly static string[] file =
        {
            "",
            "Missing",
            "",
            "- Description",
        };
        #endregion File



        private static string FormatLog(string text) => "UniversalGears: " + text;


        [MenuItem("Universal/Gears/Create/Missing Desc")]
        public static void ShowWindow()
        {

            // Create files
            CreateFile(FolderName, FileName, file, FormatLog);

            // Compile
            AssetDatabase.Refresh();

        }

    }
}