using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Weapon Data", menuName = "Items/New Ranged Weapon Data")]
public class RangedWeaponData : WeaponData
{
    [SerializeField]
    [Range(0, 90)]
    private float _deviation;

    [SerializeField]
    private float _range;

    public float Deviation { get => _deviation; }

    public float Range { get => _range; }

    public override string GetItemClass()
    {
        return "Ranged";
    }
}
