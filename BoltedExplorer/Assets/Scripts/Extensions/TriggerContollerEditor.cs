using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor( typeof(TriggerController))]
[CanEditMultipleObjects]
public class TriggerContollerEditor : Editor
{
    public SerializedProperty
        prefab_Prop,
        target_Prop,
        spawnLocation_Prop,
        type_Prop,
        limited_Prop,
        count_Prop,
        dialogue_Prop,
        dialogueDisplayTime_Prop,
        startDelayTime_Prop,
        endDelayTime_Prop,
        speed_Prop;


    void OnEnable()
    {
        prefab_Prop = serializedObject.FindProperty("prefab");
        target_Prop = serializedObject.FindProperty("target");
        spawnLocation_Prop = serializedObject.FindProperty("spawnLocation");
        type_Prop = serializedObject.FindProperty("type");
        limited_Prop = serializedObject.FindProperty("limited");
        count_Prop = serializedObject.FindProperty("count");
        dialogue_Prop = serializedObject.FindProperty("dialogue");
        dialogueDisplayTime_Prop = serializedObject.FindProperty("dialogueDisplayTime");
        startDelayTime_Prop = serializedObject.FindProperty("startDelayTime");
        endDelayTime_Prop = serializedObject.FindProperty("endDelayTime");
        speed_Prop = serializedObject.FindProperty("speed");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField( type_Prop );

        TriggerType type = ( TriggerType )type_Prop.enumValueIndex;

        switch( type )
        {
            case TriggerType.Dialogue:
                EditorGUILayout.PropertyField(prefab_Prop, new GUIContent("Prefab"));
                EditorGUILayout.PropertyField(dialogue_Prop, new GUIContent("Dialogue"));
                EditorGUILayout.PropertyField(dialogueDisplayTime_Prop, new GUIContent("Dialogue Display Time"));
                EditorGUILayout.PropertyField(target_Prop, new GUIContent("Target Object"));
                break;
            case TriggerType.Wraith:
                EditorGUILayout.PropertyField(prefab_Prop, new GUIContent("Prefab"));
                EditorGUILayout.PropertyField(limited_Prop, new GUIContent("Limited"));
                EditorGUILayout.PropertyField(count_Prop, new GUIContent("Spawn Count"));
                EditorGUILayout.PropertyField(spawnLocation_Prop, new GUIContent("Spawn Location"));
                EditorGUILayout.PropertyField(target_Prop, new GUIContent("Target Object"));
                break;
            case TriggerType.Trap:
                EditorGUILayout.PropertyField(prefab_Prop, new GUIContent("Prefab"));
                EditorGUILayout.PropertyField(limited_Prop, new GUIContent("Limited"));
                EditorGUILayout.PropertyField(count_Prop, new GUIContent("Count"));
                EditorGUILayout.PropertyField(startDelayTime_Prop, new GUIContent("Start Delay Time"));
                EditorGUILayout.PropertyField(endDelayTime_Prop, new GUIContent("End Delay Time"));
                EditorGUILayout.PropertyField(speed_Prop, new GUIContent("Speed"));
                EditorGUILayout.PropertyField(target_Prop, new GUIContent("Target Object"));
                break;
            case TriggerType.Destroy:
                EditorGUILayout.PropertyField(target_Prop, new GUIContent("Target Object"));
                EditorGUILayout.PropertyField(startDelayTime_Prop, new GUIContent("Destroy Delay"));
                break;
        };


        serializedObject.ApplyModifiedProperties();
    }
}
