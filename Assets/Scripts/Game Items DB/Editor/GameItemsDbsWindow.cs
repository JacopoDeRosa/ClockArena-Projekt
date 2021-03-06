using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class GameItemsDbsWindow : EditorWindow
{
    private static GameItemsDbsWindow instance;
    private int _selectedType;

    [MenuItem("Tools/Game Item Database")]
    public static void GetEditorWindow()
    {
        instance = GetWindow<GameItemsDbsWindow>("Game Item Database");
        instance.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();

        CheckDB<WeaponsDB>(WeaponsDB.Name);
        CheckDB<ArmourDB>(ArmourDB.Name);
        CheckDB<GadgetsDB>(GadgetsDB.Name);

        EditorGUILayout.EndHorizontal();
    }

    private void CheckDB<T>(string name) where T: GameItemDB
    {
        EditorGUILayout.BeginVertical();
        T DB = GameItemDB.GetDbOfType<T>(name);
        if (DB == null)
        {
            EditorGUILayout.LabelField(name + "Local Database does not exist");
            if (GUILayout.Button("Create DB?"))
            {
                T newDB = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(newDB, "Assets/Resources/" + GameItemDB.Path + "/" + name + ".asset");
                DB = GameItemDB.GetDbOfType<T>(name);
                Selection.activeObject = DB;
                AssetDatabase.SaveAssets();
            }
        }
        else
        {
            EditorGUILayout.LabelField(name + " Local Database exist, you are good to go");
            if (GUILayout.Button("Ping It"))
            {
                EditorGUIUtility.PingObject(DB);
            }

            if(GUILayout.Button("Select It"))
            {
                Selection.activeObject = DB;
            }

        }
        EditorGUILayout.EndVertical();
    }
}
