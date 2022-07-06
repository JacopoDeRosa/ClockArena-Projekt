using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon<T> : Weapon where T : WeaponData
{
    public new T Data { get => _data as T; }

    private void OverrideWeaponDataType()
    {
        if (_data is T == false)
        {
            _data = null;
        }
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        OverrideWeaponDataType();
    }
}