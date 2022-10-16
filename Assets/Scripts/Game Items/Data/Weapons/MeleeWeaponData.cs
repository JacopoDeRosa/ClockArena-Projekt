using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon Data", menuName = "Items/New Melee Weapon Data")]
public class MeleeWeaponData : WeaponData
{
    [SerializeField]
    private float _range;
    [SerializeField]
    private float _angle;

    public float Range { get => _range; }
    public float Angle { get => _angle; }

    public override string GetItemClass()
    {
        return "Melee";
    }
}
