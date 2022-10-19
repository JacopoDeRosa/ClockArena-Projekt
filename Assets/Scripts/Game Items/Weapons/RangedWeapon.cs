using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon<RangedWeaponData>
{
    [SerializeField] private ParticleSystem _muzzleFx;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private Transform _muzzle;

    public override void Attack()
    {
        if(Physics.Raycast(_muzzle.position, _muzzle.forward, out RaycastHit hit))
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
}
