using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor Script
/// Adds custom UI at the editor side for easier testing and control
/// </summary>
[CustomEditor(typeof(Grids))]
public class GridsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Grids GridScript = (Grids)target;
        if (GUILayout.Button("Regen grid"))
        {
            GridScript.RegenGrid();
        }
    }
}
