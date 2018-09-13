using UnityEngine;
using UnityEditor;
using Rotorz.ReorderableList;
using UnityEditorInternal;

[CustomEditor(typeof(WaveEnemySequence))]
public class WaveEnemySequenceEditor : Editor
{
    SerializedProperty sequenceList;
    SerializedProperty loopCount;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(loopCount, new GUIContent("Loop Count"));

        ReorderableListGUI.Title("Sequence Commands");
        ReorderableListGUI.ListField(sequenceList);

        serializedObject.ApplyModifiedProperties();
    }

    void OnEnable()
    {
        loopCount = serializedObject.FindProperty("loopCount");
        sequenceList = serializedObject.FindProperty("commands");
    }
}