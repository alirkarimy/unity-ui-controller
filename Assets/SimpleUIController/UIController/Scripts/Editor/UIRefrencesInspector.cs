using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AssetManager))]
[CanEditMultipleObjects]
public class UIRefrencesInspector : BaseInspector
{
    public SerializedProperty baseDialogs;
    private void OnEnable()
    {
        baseDialogs = serializedObject.FindProperty("baseDialogs");
    }

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        serializedObject.Update();
        ShowArrayProperty(baseDialogs, typeof(UIType),"baseDialogs");
        serializedObject.ApplyModifiedProperties();
    }
}