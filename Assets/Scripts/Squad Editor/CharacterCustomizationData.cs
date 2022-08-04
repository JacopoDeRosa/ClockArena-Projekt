using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterCustomizationData
{
    public string name;
    public Factions faction;
    public int dataType;
    public int icon;
    public int voice;
    public int weapon;
    public int gadget;
    public int head;
    public int body;
    public int exp;


    public CharacterCustomizationData()
    {
        name = "Unnamed Soldier";
        voice = 0;
        weapon = 0;
        gadget = 0;
        head = 0;
        body = 0;
        exp = 0;
    }

}
