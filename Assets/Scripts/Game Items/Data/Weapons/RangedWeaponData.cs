using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Weapon Data", menuName = "Items/New Ranged Weapon Data")]
public class RangedWeaponData : WeaponData
{
    [SerializeField]
    [Range(0, 90)]
    private float _deviation;

    public float Deviation { get => _deviation; }
}
