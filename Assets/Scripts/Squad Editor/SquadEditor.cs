using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadEditor : MonoBehaviour
{
    [SerializeField] private Character _characterTemplate;
    [SerializeField] private Transform[] _characterBases = new Transform[6];
    [SerializeField] private Character[] _characters = new Character[6];

    public Character GetCharacter(int index)
    {
        if(index >= _characters.Length)
        {
            return null;
        }
        else
        {
            return _characters[index];
        }
    }

    public Character SpawnCharacterAtIndex(int index)
    {
        if (index >= _characterBases.Length) return null;

        Character spawnedChar = Instantiate(_characterTemplate, _characterBases[index]);
        _characters[index] = spawnedChar;

        return spawnedChar;
    }
}
