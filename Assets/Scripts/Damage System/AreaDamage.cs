using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _delay;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _layerMask;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    private void Awake()
    {
        StartCoroutine(DealDamage());
    }

    private IEnumerator DealDamage()
    {
        yield return new WaitForSeconds(_delay);
        Collider[] colliders = Physics.OverlapSphere(transform.position, _range / 2, _layerMask);

        Damage damage = new Damage(_damage);
       
        foreach (Collider collider in colliders)
        {     
            foreach (IDamageable damageable in collider.GetComponents<IDamageable>())
            {
                damageable.DealDamage(damage);
            }
        }
    }
}
