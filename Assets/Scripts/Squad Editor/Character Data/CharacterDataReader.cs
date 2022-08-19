using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataReader : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private CharacterVoice _voice;
    [SerializeField] private Equipment _equipment;
    [SerializeField] private CharacterStats _stats;
    
    public void ReadData(CharacterComponentsData data)
    {
        _character.SetName(data.name);
        _character.SetIcon(data.icon);
        _character.SetExp(data.exp);
        _character.SetFaction(data.faction);
        _character.SetLevel(data.level);

        _equipment.SetWeapon(data.weapon);
        _equipment.SetGadget(data.gadget);
        _equipment.SetHeadArmour(data.head);
        _equipment.SetBodyArmour(data.body);

        _voice.SetVoicePack(data.voice);

        _stats.SetBaseStats(data.dataType);

    }
}
