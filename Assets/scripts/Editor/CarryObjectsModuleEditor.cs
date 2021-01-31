using UnityEditor;
using UnityEngine;

namespace GGJ2021
{
    [CustomEditor(typeof(CarryObjectsModule))]
    public class CarryObjectsModuleEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginVertical("box");
            EditorGUI.BeginDisabledGroup(true);
            for (int i = 0; i < TypeTarget.pickablesNearby.Count; i++)
            {
                EditorGUILayout.ObjectField(TypeTarget.pickablesNearby[i].transform, typeof(Transform), true);
            }

            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndVertical();
        }

        public override bool RequiresConstantRepaint()
        {
            return true;
        }

        public CarryObjectsModule TypeTarget { get { return target as CarryObjectsModule; } }
    }
}