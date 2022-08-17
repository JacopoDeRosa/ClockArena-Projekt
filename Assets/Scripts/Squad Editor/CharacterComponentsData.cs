using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterComponentsData
{
    public bool available;
    public string name;
    public Faction faction;
    public int dataType;
    public int icon;
    public int voice;
    public int weapon;
    public int gadget;
    public int head;
    public int body;
    public int exp;
    public int level;

    public CharacterComponentsData()
    {
        name = "Soldier";
        available = false;
        icon = 0;
        voice = 0;
        weapon = 0;
        gadget = 0;
        head = 0;
        body = 0;
        exp = 0;
        level = 1;
        dataType = 0;
    }
    public CharacterComponentsData(bool available)
    {
        name = "Soldier";
        this.available = available;
        icon = 0;
        voice = 0;
        weapon = 0;
        gadget = 0;
        head = 0;
        body = 0;
        exp = 0;
        level = 1;
        dataType = 0;
    }
    public CharacterComponentsData(bool available, string name, Faction faction)
    {
        this.name = name;
        this.available = available;
        this.faction = faction;
        icon = 0;
        voice = 0;
        weapon = 0;
        gadget = 0;
        head = 0;
        body = 0;
        exp = 0;
        level = 1;
        dataType = 0;
    }

}
