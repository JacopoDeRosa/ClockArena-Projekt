using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    new public MeleeWeaponData Data { get => _data as MeleeWeaponData; }

    public override void Attack()
    {
      
    }

    private void OnValidate()
    {
        ForceDatatype<MeleeWeaponData>();
    }
}
