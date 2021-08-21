using UnityEngine;
using UnityEditor;

public class DialogueSystemWindow : EditorWindow
{

    [MenuItem("Window/Dialogue System")]
    public static void ShowWindow()
    {
        GetWindow<DialogueSystemWindow>("Dialogue System");
    }
    void OnGUI()
    {
        GUILayout.Label("Dialogue System", EditorStyles.boldLabel);

    }
}
