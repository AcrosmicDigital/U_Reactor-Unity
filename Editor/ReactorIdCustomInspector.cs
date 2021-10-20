using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace U.Reactor
{
    [CustomEditor(typeof(ReactorId))]
    [CanEditMultipleObjects()]
    public class ReactorIdCustomInspector : Editor
    {
        public override void OnInspectorGUI()
        {

            ReactorId c = (ReactorId)target;

            GUILayout.Space(8);
            if (c.elementType != null)
                GUILayout.Label("Element Type: " + c.elementType.Name);

            GUILayout.Space(4);

            if (c.id != null)
                GUILayout.Label("Reactor Id: " + c.id);

            GUILayout.Space(4);

            if (c.className!= null)
            {
                GUILayout.Label("ClassNames: ");
                EditorGUI.indentLevel++;
                foreach (var cn in c.className)
                {
                    if (cn == null) continue;
                    GUILayout.Label(" - " + cn);
                }
                EditorGUI.indentLevel--;
            }



        }
    }
}
