using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor Script
/// Adds custom UI at the editor side for easier testing and control
/// </summary>
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
