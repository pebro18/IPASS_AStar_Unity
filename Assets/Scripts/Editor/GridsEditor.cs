using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
