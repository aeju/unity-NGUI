using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIGrid))]
public class UIGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        // Get the UIGrid component
        UIGrid grid = (UIGrid)target;

        // Add some space
        EditorGUILayout.Space();

        // Add a button to reposition the grid
        if (GUILayout.Button("Reposition Grid"))
        {
            // Call the Reposition method
            grid.Reposition();

            // Mark the scene as dirty to ensure changes are saved
            EditorUtility.SetDirty(grid.gameObject);

            // Repaint the scene view to show the changes immediately
            SceneView.RepaintAll();
        }
    }
}