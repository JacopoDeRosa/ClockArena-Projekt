using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon<RangedWeaponData>
{
    [SerializeField] protected LayerMask _hitMask;
    [SerializeField] protected ParticleSystem _muzzleFx;
    [SerializeField] protected AudioClip _clip;
    [SerializeField] protected Transform _muzzle;


    public override void Attack()
    {
        if(Physics.Raycast(_muzzle.position, _muzzle.forward, out RaycastHit hit, Data.Range, _hitMask))
        {
            if(hit.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.DealDamage(new Damage((int)Random.Range(Data.DamageRange.x, Data.DamageRange.y)));
            }
        }
        Debug.Log("Pew");
        AudioSource.PlayClipAtPoint(_clip, _muzzle.position);
        _muzzleFx.Play();
    }

    public void AimAtPoint(Vector3 point)
    {
        transform.rotation = Quaternion.LookRotation(point - transform.position);
    }
}
