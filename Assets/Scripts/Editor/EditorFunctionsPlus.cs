using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System;
public static class EditorFunctionsPlus
{
    [MenuItem("Assets/Create/New Text File", false, 1)]
    private static void CreateTextFile()
    {
        string folderPath = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
        File.CreateText(folderPath + "/New Text File.txt");
        AssetDatabase.Refresh();
    }
}