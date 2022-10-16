using System;

public interface IDamageable
{
    public void DealDamage(Damage damage);
    public Guid GetDamageId();
}

