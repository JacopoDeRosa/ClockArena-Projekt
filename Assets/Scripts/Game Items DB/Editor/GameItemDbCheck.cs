using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[InitializeOnLoad]
public static class GameItemDbCheck
{
    static GameItemDbCheck()
    {
        CheckDBs();
    }

    private static void CheckDBs()
    {
        CheckDB<WeaponsDB>();
        CheckDB<ArmourDB>();
        CheckDB<GadgetsDB>();
        CheckDB<VoiceDB>();
        CheckDB<IconsDB>();
        CheckDB<DataDB>();
        CheckDB<AbilityTreesDB>();

        Debug.Log("Checked all Item DB");
    }

    private static void CheckDB<T>() where T: GameItemDB
    {
        T DB = GameItemDB.GetDbOfType<T>();
        if (DB == null)
        {
            string name = typeof(T).Name;
            Debug.Log("Creating DB for: " + name);
            T newDB = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(newDB, "Assets/Resources/" + GameItemDB.Path + "/" + name + ".asset");
            DB = GameItemDB.GetDbOfType<T>();
            Selection.activeObject = DB;
            AssetDatabase.SaveAssets();
        }
    }
}
