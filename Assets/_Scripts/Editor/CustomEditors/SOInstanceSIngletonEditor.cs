using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Ribbons.RoguelikeGame.EditorScripts
{
    [CustomEditor(typeof(SOInstanceSingleton))]
    public class SOInstanceSIngletonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(true);
            base.OnInspectorGUI();
            EditorGUI.EndDisabledGroup();
        }
    }
}
