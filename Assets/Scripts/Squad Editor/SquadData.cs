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

    public CharacterComponentsData AddNewCharacterAtIndex(int index)
    {
        if (index >= characters.Length) return null;
        CharacterComponentsData character = new CharacterComponentsData(true, "New Character", faction);
        characters[index] = character;
        return character;
    }
    public CharacterComponentsData GetCharacterAtIndex(int index)
    {
        if (index >= characters.Length) return null;
        if (characters[index].available == false) return null;

        return characters[index];
    }
    public void UpdateCharacterAtIndex(int index)
    {
        if (index >= characters.Length) return;

        if (characters[index] == null || characters[index].available == false) return;
    }
}

