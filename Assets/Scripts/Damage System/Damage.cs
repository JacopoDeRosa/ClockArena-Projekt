using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Damage
{
    private int _damage;
    private string _id;

    public int DamageAmount { get => _damage; }
    public string ID { get => _id; }

    public Damage(int damage, string id)
    {
        _damage = damage;
        _id = id;
    }
    public Damage(int damage)
    {
        _damage = damage;
        _id = RandomID.GetBase62(8);
    }

    public bool Equals(Damage other)
    {
        return other.ID.Equals(_id);
    }

    public void MultiplyDamage(float multiplier)
    {
        _damage = Mathf.FloorToInt(_damage * multiplier);
    }
}
