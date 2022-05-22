using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon<T> : Weapon where T : WeaponData
{
    [SerializeField] new protected  T _data;
    public new T Data { get => _data; }
}