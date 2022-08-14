using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void AddNewCharacterAtIndex(int index, CharacterComponentsData character)
    {
        characters[index] = character;
    }
}
