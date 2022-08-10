using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataReader : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Equipment _equipment;
    public void ReadData(CharacterComponentsData data)
    {
        _character.SetName(data.name);
        _character.SetIcon(data.icon);
        _character.SetVoice(data.voice);
        _character.SetData(data.dataType);
        _character.SetExp(data.exp);
        _character.SetFaction(data.faction);

        _equipment.SetWeapon(data.weapon);
        _equipment.SetGadget(data.gadget);
        _equipment.SetHeadArmour(data.head);
        _equipment.SetBodyArmour(data.body);

    }
}
