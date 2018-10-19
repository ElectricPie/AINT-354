using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RPGTool : EditorWindow {
    [MenuItem("Window/RPG Tool")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(RPGTool));
    }

    private void OnGUI()
    {
        
    }
}
