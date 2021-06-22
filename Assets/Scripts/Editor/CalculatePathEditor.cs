using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ReadAndCalculatePath))]
public class CalculatePathEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ReadAndCalculatePath PathCalculate = (ReadAndCalculatePath)target;
        if (GUILayout.Button("Regen LineRenderer"))
        {
            PathCalculate.ReloadPath();
        }
    }
}
