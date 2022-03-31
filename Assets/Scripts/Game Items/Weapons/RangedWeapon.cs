using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] private Transform _muzzle;

    new public RangedWeaponData Data { get => _data as RangedWeaponData; }

    public override void Attack()
    {
       
    }
    private void OnValidate()
    {
        ForceDatatype<RangedWeaponData>();
    }
}
