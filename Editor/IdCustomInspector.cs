using UnityEngine;
using UnityEditor;

namespace U.Reactor.Editor
{
    [CustomEditor(typeof(HC.ReactorId))]
    [CanEditMultipleObjects()]
    internal class IdCustomInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {

            HC.ReactorId c = (HC.ReactorId)target;

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
