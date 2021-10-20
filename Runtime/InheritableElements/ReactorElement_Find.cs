using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    // Here are the find elements functions
    public abstract partial class REbase
    {

        // null or ""
        // .classNama
        // #id
        // .className|className|.className
        // .className&.className&className
        public static ElementSelector[] Find(string pattern)
        {
            // All
            if (String.IsNullOrEmpty(pattern))
            {

                return Resources.FindObjectsOfTypeAll<ElementId>().Select(s => s.selector).ToArray();
            }

            // By id
            else if (pattern.StartsWith("#") && pattern.TrimStart('#').Length > 0)
            {
                return Resources.FindObjectsOfTypeAll<ElementId>().Where(e => e.id == pattern.TrimStart('#')).Select(s => s.selector).ToArray();
            }

            // Search by OR || className
            else if (pattern.StartsWith(".") && pattern.Where(c => c == '|').Count() > 0 && pattern.Where(c => c == '&').Count() == 0)
            {

                return Resources.FindObjectsOfTypeAll<ElementId>().Where(elementIdCmp =>
                {
                    try
                    {

                        foreach (var classNamePattern in pattern.Split('|'))
                        {
                            if (classNamePattern.Length == 0)
                                continue;

                            foreach (var className in elementIdCmp.className)
                            {
                                if (String.IsNullOrEmpty(className))
                                    continue;

                                if (className == classNamePattern.TrimStart('.'))
                                    return true;
                            }
                        }

                        return false;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }).Select(s => s.selector).ToArray();

            }

            // Search by AND && className
            else if (pattern.StartsWith(".") && pattern.Where(c => c == '&').Count() > 0 && pattern.Where(c => c == '|').Count() == 0)
            {
                return Resources.FindObjectsOfTypeAll<ElementId>().Where(elementIdCmp =>
                {
                    try
                    {

                        if (elementIdCmp.className.Length == 0)
                            return false;

                        foreach (var classNamePattern in pattern.Split('&'))
                        {

                            if (classNamePattern.Length == 0)
                                continue;

                            bool match = false;
                            foreach (var className in elementIdCmp.className)
                            {
                                if (String.IsNullOrEmpty(className))
                                    continue;


                                if (className == classNamePattern.TrimStart('.'))
                                    match = true;

                            }

                            if (!match)
                                return false;
                        }

                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }).Select(s => s.selector).ToArray();

            }

            // By one className
            else if (pattern.StartsWith(".") && pattern.TrimStart('.').Length > 0)
            {
                return Resources.FindObjectsOfTypeAll<ElementId>().Where(elementIdCmp =>
                {
                    try
                    {
                        foreach (var className in elementIdCmp.className)
                        {
                            if (String.IsNullOrEmpty(className))
                                continue;

                            if (className == pattern.TrimStart('.'))
                                return true;
                        }
                        return false;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }).Select(s => s.selector).ToArray();
            }

            // Return empty array
            else
            {
                return new ElementSelector[0];
            }

        }

        public static ElementSelector[] Find()
        {
            return Find(null);
        }

        public static ElementSelector FindOne(string pattern)
        {
            return Find(pattern).FirstOrDefault();
        }


        protected static TSelector[] Find<TSelector>(string pattern) where TSelector : ElementSelector
        {
            return Find(pattern).Select(e =>
            {
                try
                {
                    return (TSelector)e;
                }
                catch (Exception)
                {
                    return null;
                }

            }).Where(e => e != null).ToArray();
        }

        protected static TSelector[] Find<TSelector>() where TSelector : ElementSelector
        {
            return Find<TSelector>(null);
        }

        protected static TSelector FindOne<TSelector>(string pattern) where TSelector : ElementSelector
        {
            return Find<TSelector>(pattern).FirstOrDefault();
        }

    }
}
