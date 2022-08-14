using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private CharacterBaseStats _baseStats;

    [SerializeField] private int _hp, _stamina, _ap, _sanity;

    public int HP { get => _hp; }
    public int Stamina { get => _stamina; }
    public int AP { get => _ap; }
    public int Sanity { get => _sanity; }

    public void SetBaseStats(int dataType)
    {
        _baseStats = GameItemDB.GetDbOfType<DataDB>(DataDB.Name).GetItem(dataType);
    }
}
