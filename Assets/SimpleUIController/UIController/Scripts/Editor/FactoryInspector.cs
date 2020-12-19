using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIFactory))]
[CanEditMultipleObjects]
public class FactoryInspector : BaseInspector
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