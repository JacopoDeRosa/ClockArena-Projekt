using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadData
{
    public Factions faction;
    public int image;
    public string name;
    public CharacterComponentsData[] characters = new CharacterComponentsData[6];

    public SquadData(int image, Factions faction, string name)
    {
        this.image = image;
        this.faction = faction;
        this.name = name;
        characters = new CharacterComponentsData[6];
    }
}
