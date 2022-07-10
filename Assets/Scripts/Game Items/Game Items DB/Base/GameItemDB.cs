using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItemDB : ScriptableObject
{
    protected const int IdLenght = 8;

    public const string Path = "Item DBs/";

    [SerializeField] private string _id;

    public string Id { get => _id; }

    public virtual void OnValidate()
    {
        if(string.IsNullOrEmpty(_id))
        {
            _id = RandomID.GetBase62(IdLenght);
        }
    }

    public static T GetDbOfType<T>(string name) where T : GameItemDB
    {
         return Resources.Load<T>(Path + name);
    }
}
