using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class WeaponData : ItemData
{
    [SerializeField] private Vector2 _damageRange;

    [SerializeField]
    [ReadOnly]
    private WeaponTypes _weaponType;

    public Vector2 DamageRange { get => _damageRange; }

    protected void ForceWeaponType(WeaponTypes type)
    {
        _weaponType = type;
    }
}
