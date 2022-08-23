using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnLifetime : MonoBehaviour
{
    [SerializeField] private int _lifeTime;
    [SerializeField] private bool _destroyOnDeath;

    private GameTurnManager _gameTurns;

    public UnityEvent onDeath;
    public UnityEvent<int> onLifeTimeChange;

    private void Awake()
    {
        _gameTurns = FindObjectOfType<GameTurnManager>();
        if(_gameTurns != null)
        {
            _gameTurns.onTurnEnded.AddListener(OnTurnEnd);
        }
    }

    private void OnTurnEnd()
    {
        _lifeTime--;
        if(_lifeTime == 0)
        {
            onDeath?.Invoke();
            if (_destroyOnDeath) Destroy(gameObject);
        }
        else
        {
            onLifeTimeChange.Invoke(_lifeTime);
        }
    }
    


}
