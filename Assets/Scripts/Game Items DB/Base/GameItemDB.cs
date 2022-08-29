using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItemDB : ScriptableObject
{
    public const string Path = "Item DBs/";

    public static T GetDbOfType<T>() where T : GameItemDB
    {
         return Resources.Load<T>(Path + typeof(T).Name);
    }
}
