using UnityEditor;
using UnityEngine;

namespace GGJ2021
{

    [CustomEditor(typeof(IsGroundedScanner))]
    public class IsGroundedScannerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginVertical("box");
            EditorGUI.BeginDisabledGroup(true);
            GUILayout.Toggle(TypeTarget.IsGrounded, "is grounded");
            EditorGUILayout.LabelField("ground count", TypeTarget.GroundCount.ToString());
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndVertical();
        }

        public IsGroundedScanner TypeTarget { get { return target as IsGroundedScanner; } }
    }
}