using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SquadData
{
    public Faction faction;
    public int image;
    public string name;
    public CharacterComponentsData[] characters = new CharacterComponentsData[6];

    public SquadData(Faction faction, string name)
    {
        this.image = 0;
        this.faction = faction;
        this.name = name;
        characters = new CharacterComponentsData[6];
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] = new CharacterComponentsData();
            characters[i].faction = faction;
        }
    }

    public void AddNewCharacterAtIndex(int index, CharacterComponentsData character)
    {
        characters[index] = character;
    }
}
