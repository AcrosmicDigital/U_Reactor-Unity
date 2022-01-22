using UnityEditor;
using static U.Reactor.Editor.UE;

namespace U.Reactor.Editor
{
    public class CreateRouterMenuButton : EditorWindow
    {

        #region .router File
        private static string DefaultFolderName => "/Scripts/Reactor/Routers/";
        private static string DefaultFileName => "NewRouter";
        static string[] File(string fileName) => new string[]
        {
            "using U.Reactor;",
            "using System.Collections.Generic;",
            "",
            "public static partial class RRouting",
            "{",
            "",
            "    public static readonly Router "+fileName+" = new Router",
            "    {",
            "        routes = new Dictionary<string, REcanvas>()",
            "            {",
            "                // Write here the views",
            "                //{Routes.Home, new RViews.Home().Create() },",
            "                //{Routes.NotFound, new RViews.NotFound().Create() },",
            "                // ...",
            "            },",
            "        //defaultRoute = Routes.NotFound,  // Go to this route if a route is invalid",
            "        //inDontDestroyOnLoad = true,",
            "    };",
            "",
            "    public static partial class Routes",
            "    {",
            "        // Write here the routes to each view",
            "        public static string Home => "+quote+"Home"+quote+";",
            "        public static string NotFound => "+quote+"NotFound"+quote+";",
            "        // ...",
            "    }",
            "",
            "}",
        };
        #endregion .router File



        private static string FormatLog(string text) => "Reactor: " + text;


        [MenuItem("Universal/Reactor/Create/Router")]
        public static void ShowWindow()
        {

            // Create files
            CreateFileWithSaveFilePanel(DefaultFolderName, DefaultFileName, File, FormatLog);

            // Compile
            AssetDatabase.Refresh();

        }

    }
}