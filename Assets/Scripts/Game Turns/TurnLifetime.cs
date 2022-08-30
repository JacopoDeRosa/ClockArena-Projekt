using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnLifetime : MonoBehaviour
{
    [SerializeField] private int _lifeTime;
    [SerializeField] private bool _destroyOnDeath;
    [SerializeField] private GameObject _instantiateOnDeath;

    private GameTurnManager _gameTurns;

    public UnityEvent onDeath;
    public UnityEvent<int> onLifeTimeChange;

    private void Awake()
    {
        _gameTurns = FindObjectOfType<GameTurnManager>();
        if(_gameTurns != null)
        {
            _gameTurns.onTurnStarted.AddListener(OnTurnStart);
        }
    }

    private void OnDestroy()
    {
        if (_gameTurns != null)
        {
            _gameTurns.onTurnStarted.RemoveListener(OnTurnStart);
        }
    }

    private void OnTurnStart(int turn)
    {
        _lifeTime--;
        if(_lifeTime == 0)
        {
            onDeath?.Invoke();
            if (_destroyOnDeath) Destroy(gameObject);
            if (_instantiateOnDeath != null) Instantiate(_instantiateOnDeath, transform.position, transform.rotation);
        }
        else
        {
            onLifeTimeChange.Invoke(_lifeTime);
        }
    }
    


}
