using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEachTurn : MonoBehaviour
{
    private const float checkInterval = 0.25f;

    [SerializeField] private float _radius;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _effectMask;

    [SerializeField]
    private List<Collider> _affectedColliders;

    private WaitForSeconds _checkWait;

    private Collider[] _checkedColliders;

    private GameTurnManager _turnManager;


    private void Awake()
    {
        _turnManager = FindObjectOfType<GameTurnManager>();
        _turnManager.onTurnStarted.AddListener(OnNewTurn);
        _checkWait = new WaitForSeconds(checkInterval);
        _checkedColliders = new Collider[25];
        _affectedColliders = new List<Collider>();
        StartCoroutine(CheckDamageRoutine());
    }

    private void OnDestroy()
    {
        _turnManager.onTurnStarted.RemoveListener(OnNewTurn);
    }

    private void CheckForDamage()
    {
      
         _checkedColliders =  Physics.OverlapSphere(transform.position, _radius, _effectMask);
        foreach (Collider collider in _checkedColliders)
        {
            if(collider == null || _affectedColliders.Contains(collider))
            {
                continue;
            }

            foreach (IDamageable damageable in collider.GetComponents<IDamageable>())
            {
                damageable.DealDamage(_damage);
            }
            _affectedColliders.Add(collider);
        }
    }    
    private void OnNewTurn(int turn)
    {
        _affectedColliders.Clear();
    }

    private IEnumerator CheckDamageRoutine()
    {
        while(gameObject.activeInHierarchy)
        {
            CheckForDamage();
            yield return _checkWait;
        }
    }
}
