﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace U.Reactor
{
    public class Router
    {

        // What the router will do wuth unused views 
        public enum RouterMode
        {
            Erase, // Delete unused views (Delete the gameobjects)
            Hide, // Hide unused views (Disable canvas component)
            Disable, // Disable unused views (Disable canvas Gameobject)
        }


        public Dictionary<string, REcanvas> routes = new Dictionary<string, REcanvas>(); // List of routes 
        public string defaultRoute = "";  // The default route is empty by default
        public RouterMode routerMode = RouterMode.Erase;  // Is erase by default
        public bool inDontDestroyOnLoad = false;


        private REcanvas defaultRouteCanvas = new REcanvas
        {
            propsCanvasScaler = () => new REcanvas.CanvasScalerSetter
            {
                matchWidthOrHeight = 1,
            },
            propsGameObject = () => new REcanvas.GameObjectSetter
            {
                name = "Route Not Found",
            },
            childs = () => new REbase[]
            {
                    new RElabel
                    {
                        propsText = () => new RElabel.TextSetter
                        {
                            text = "Route Not Found",
                            fontColor = Color.white,
                        },
                        propsImage = () => new RElabel.BackImageSetter
                        {
                            color = Color.red,
                        }
                    },
            }
        };  // A default canvas to show when no route is available
        private Stack<string> statesQueue = new Stack<string>(); // Stack of prev routes



        // Functions to hide, destroy or disable the unused views
        public void Erase()
        {
            if (routes == null)
                return;

            foreach (var urcanvas in routes)
            {
                if (urcanvas.Value == null)
                    continue;

                urcanvas.Value.Erase();
            }
            defaultRouteCanvas.Erase();
        }
        public void Hide()
        {
            if (routes == null)
                return;

            foreach (var urcanvas in routes)
            {
                if (urcanvas.Value == null)
                    continue;

                urcanvas.Value.Hide();
            }
            defaultRouteCanvas.Hide();
        }
        public void Disable()
        {
            if (routes == null)
                return;

            foreach (var urcanvas in routes)
            {
                if (urcanvas.Value == null)
                    continue;

                urcanvas.Value.Disable();
            }
            defaultRouteCanvas.Disable();
        }


        // Function to draw all the views, but hidded
        public void PreDraw()
        {

            // Only can be called before any route
            if (statesQueue.Count() > 0)
                return;

            if (routes == null)
                return;

            foreach (var urcanvas in routes)
            {

                if (urcanvas.Value == null)
                    continue;

                urcanvas.Value.Draw();
                if (inDontDestroyOnLoad) urcanvas.Value.ToDontDestroyOnLoad();
            }

            defaultRouteCanvas.Draw();
            if (inDontDestroyOnLoad) defaultRouteCanvas.ToDontDestroyOnLoad();
            Hide();

            //if (routerMode == RouterMode.Disable)
            //    Disable();
            //else if (routerMode == RouterMode.Hide)
            //    Hide();
            //else
            //    Erase();

        }


        // Function to choose the view to show
        protected void DoRouting()
        {

            // First disable all the views
            if (routerMode == RouterMode.Disable)
            {
                Disable();
            }
            else if (routerMode == RouterMode.Hide)
            {
                Hide();
            }
            else
            {
                Erase();
            }

            // By default the route is the default view
            REcanvas canvas = defaultRouteCanvas;

            // If other default view is set, that will be 
            if (!String.IsNullOrEmpty(defaultRoute))
                if (routes.ContainsKey(defaultRoute))
                    canvas = routes[defaultRoute];

            // Is the route requested exist, that will be
            var peek = "Default";
            if (statesQueue.Count() > 0)
            {
                peek = statesQueue.Peek();
                if (routes.ContainsKey(statesQueue.Peek()))
                    canvas = routes[statesQueue.Peek()];
            }


            try
            {
                // Show the view if is hidded or disabled
                canvas.Draw();
                canvas.Enable();
                canvas.Show();

                if (inDontDestroyOnLoad) canvas.ToDontDestroyOnLoad();

            }
            catch (Exception e)
            {
                Debug.LogError("ReactorRouter: Error while rounting to :" + peek + " , " + e);
            }


        }


        // Can be used to reload, dont add new view to stack
        public void Route()
        {
            DoRouting();
        }

        // Used to add a new rpute and draw it
        public void Route(string path)
        {
            if (String.IsNullOrEmpty(path))
            {
                Route();
                return;
            }

            statesQueue.Push(path);

            DoRouting();

        }


        // Remove a view from stack and draw
        public void Return()
        {
            if (statesQueue.Count > 1)
                statesQueue.Pop();

            DoRouting();

        }

        // Remove x number of views from the stack and draw
        public void Return(int steps = 1)
        {
            for (int i = 0; i < steps; i++)
            {
                if (statesQueue.Count > 1)
                    statesQueue.Pop();
            }

            DoRouting();

        }

    }
}
