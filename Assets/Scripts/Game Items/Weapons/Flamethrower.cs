using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : RangedWeapon
{
    [SerializeField] private LayerMask _flameBlockerMask;

    public override void Attack()
    {
        Ray forwardRay = new Ray(_muzzle.position, _muzzle.forward);

        float maxDistance = Data.Range;

        if (Physics.SphereCast(forwardRay, 0.25f,out RaycastHit hit, Data.Range, _flameBlockerMask))
        {
            maxDistance = hit.distance;
        }

        RaycastHit[] hits = Physics.SphereCastAll(forwardRay, 0.25f, maxDistance, _hitMask);

        Damage damageThisShot = new Damage((int)Random.Range(Data.DamageRange.x, Data.DamageRange.y));
        foreach (RaycastHit possibleHit in hits)
        {
            if (possibleHit.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.DealDamage(damageThisShot);
            }
        }

        AudioSource.PlayClipAtPoint(_clip, _muzzle.position);
        _muzzleFx.Play();
    }
}
