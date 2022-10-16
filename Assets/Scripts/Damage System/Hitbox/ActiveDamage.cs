using UnityEngine;

public class ActiveDamage
{
    private const float _damageDuration = 5f;

    private Damage _damage;
    private float _lifeTime;

    public Damage Damage { get => _damage; }

    public ActiveDamage(Damage damage)
    {
        _damage = damage;
    }

    public bool Equals(Damage damage)
    {
        return damage.Equals(_damage);
    }
    public override bool Equals(object obj)
    {
        ActiveDamage other = obj as ActiveDamage;
        if (other == null) return false;
        return Equals(other.Damage);
    }
    public override int GetHashCode()
    {
        return _damage.ID.GetHashCode();
    }
    public bool UpdateLifetime()
    {
        _lifeTime += Time.deltaTime;
        return _lifeTime >= _damageDuration;
    }
}
