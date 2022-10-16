using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void HitboxEventHandler(HitboxEventData eventData);

public struct HitboxEventData
{
    private Damage _damage;
    private Bodyparts _bodyPart;

    public Damage Damage { get => _damage; }
    public Bodyparts Bodypart { get => _bodyPart; }

    public HitboxEventData(Damage damage, Bodyparts bodypart)
    {
        _damage = damage;
        _bodyPart = bodypart;
    }

}

public class HitboxArea : MonoBehaviour, IDamageable
{
    [SerializeField] private Bodyparts _bodyPart;

    public event HitboxEventHandler onHit;

    private Guid _id;

    public void DealDamage(Damage damage)
    {
        onHit?.Invoke(new HitboxEventData(damage, _bodyPart));
    }
    public Guid GetDamageId()
    {
        return _id;
    }
    public void SetId(Guid guid)
    {
        if (_id != null)
        {
            _id = guid;
        }
    }
}
