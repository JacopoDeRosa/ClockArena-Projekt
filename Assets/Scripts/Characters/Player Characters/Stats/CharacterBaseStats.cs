using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Base Stats", menuName = "New Character Base Stats")]
public class CharacterBaseStats : ScriptableObject
{
    [SerializeField] private int _baseHP, _baseStamina, _baseAP, _baseSanity;
    [SerializeField] private int _staminaRegen = 30;

    public int BaseHP { get => _baseHP; }
    public int BaseStamina { get => _baseStamina; }
    public int BaseAP { get => _baseAP; }
    public int BaseSanity { get => _baseSanity; }
    public int StaminaRegen { get => _staminaRegen; }
}

