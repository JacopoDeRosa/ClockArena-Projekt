using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon Data", menuName = "Items/New Melee Weapon Data")]
public class MeleeWeaponData : WeaponData
{
    [SerializeField]
    [Range(0, 100)]
    private float _hitChance = 50f;

    [SerializeField]
    private float _range;

    public float HitChance { get => _hitChance; }

    protected override void OnValidate()
    {
        base.OnValidate();
        ForceWeaponType(WeaponTypes.Melee);
    }
}
