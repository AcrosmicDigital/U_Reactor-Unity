using UnityEditor;
using static U.Reactor.Editor.UE;

namespace U.Reactor.Editor
{
    public class CreateRouterMenuButton : EditorWindow
    {

        #region File
        private static string DefaultFolderName => "/Scripts/Reactor/Routers/";
        private static string DefaultFileName => "New";
        private static string CustomExtension => "router";
        static string[] File(string fileName) => new string[]
        {
            "using U.Reactor;",
            "using System.Collections.Generic;",
            "",
            "public static partial class RRrouting",
            "{",
            "",
            "    public static readonly Router "+fileName+" = new Router",
            "    {",
            "        routes = new Dictionary<string, REcanvas>()",
            "            {",
            "                // Write here the views",
            "                //{Routes.Home, new RRViews.Home().Create() },",
            "                //{Routes.NotFound, new RRViews.NotFound().Create() },",
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
        #endregion File



        private static string FormatLog(string text) => "Reactor: " + text;


        [MenuItem("Universal/Reactor/Create/Router")]
        public static void ShowWindow()
        {

            // Create files
            CreateFileWithSaveFilePanelAndCustomExtension(DefaultFolderName, DefaultFileName, File, FormatLog, CustomExtension);

            // Compile
            AssetDatabase.Refresh();

        }

    }
}