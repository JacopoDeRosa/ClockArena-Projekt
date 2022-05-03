using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LightManagerWindow : EditorWindow
{
   [MenuItem("Map Utility/Light Manager Window")]
    public static EditorWindow GetEditorWindow()
    {
        var window = GetWindow<LightManagerWindow>(false);
        return window;
    }


    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Street Lights On"))
        {
            ToggleLights("Street", true);
        }
        if(GUILayout.Button("Stree Lights Off"))
        {
            ToggleLights("Street", false);
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Utility Lights On"))
        {
            ToggleLights("Utility", true);
        }
        if (GUILayout.Button("Utility Lights Off"))
        {
            ToggleLights("Utility", false);
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Sewer Lights On"))
        {
            ToggleLights("Sewer", true);
        }
        if (GUILayout.Button("Sewer Lights Off"))
        {
            ToggleLights("Sewer", false);
        }
        EditorGUILayout.EndHorizontal();
    }

    private void ToggleLights(string tag, bool status)
    {
        GameObject[] obs = GameObject.FindGameObjectsWithTag("Light_" + tag);
        foreach (var ob in obs)
        {
            ob.SetActive(status);
        }
    }
}
