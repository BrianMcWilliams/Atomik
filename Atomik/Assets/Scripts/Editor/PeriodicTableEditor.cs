using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PeriodicTable))]
public class DialogueAssetEditor : Editor
{

    public override void OnInspectorGUI()
    {
        // Show default inspector property editor, 
        // so we don't need to draw the DialogueData field ourselves.
        DrawDefaultInspector();

        var asset = (PeriodicTable)target;

        // Get a typed reference to the DialogueAsset we're editing.
        // Add a button we can click to fire the "Import" method.
        if (GUILayout.Button(asset.m_PeriodicTable == null ? "Import" : "Reimport")) {
            // Import the data.
            asset.Import();
        }
    }
}