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
        CheckDB<WeaponsDB>(WeaponsDB.Name);
        CheckDB<ArmourDB>(ArmourDB.Name);
        CheckDB<GadgetsDB>(GadgetsDB.Name);
        CheckDB<VoiceDB>(VoiceDB.Name);
        CheckDB<IconsDB>(IconsDB.Name);
        CheckDB<DataDB>(DataDB.Name);
        CheckDB<AbilityTreesDB>(AbilityTreesDB.Name);

        Debug.Log("Checked all Item DB");
    }

    private static void CheckDB<T>(string name) where T: GameItemDB
    {
        T DB = GameItemDB.GetDbOfType<T>(name);
        if (DB == null)
        {
            Debug.Log("Creating DB for: " + name);
            T newDB = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(newDB, "Assets/Resources/" + GameItemDB.Path + "/" + name + ".asset");
            DB = GameItemDB.GetDbOfType<T>(name);
            Selection.activeObject = DB;
            AssetDatabase.SaveAssets();
        }
    }
}
