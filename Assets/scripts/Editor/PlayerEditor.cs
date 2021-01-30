using UnityEditor;
using UnityEngine;

namespace GGJ2021
{

    [CustomEditor(typeof(Player))]
    public class PlayerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(!Application.isPlaying) return;

            if(TypeTarget.Mover == null)
                EditorGUILayout.HelpBox("Missing mover component!", MessageType.Error);
        }

        public Player TypeTarget { get { return target as Player; } }
    }
}