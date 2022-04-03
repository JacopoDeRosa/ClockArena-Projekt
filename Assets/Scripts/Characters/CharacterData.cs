using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "New Character Data")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private int _baseHP, _baseStamina, _baseAP, _baseSanity;
    [SerializeField] private Factions _faction;
    [SerializeField] private Sprite _defaultIcon;

    public Factions Faction { get => _faction; }
    public Sprite DefaulIcon { get => _defaultIcon; }
}
