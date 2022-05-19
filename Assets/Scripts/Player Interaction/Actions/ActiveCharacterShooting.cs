using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveCharacterShooting : PermanentAction
{
    [SerializeField] private GameTurnManager _turnManager;
    [SerializeField] private MousePointGetter _pointGetter;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private WorldGizmos _worldGizmos;

    protected override void OnValidate()
    {
        base.OnValidate();
        if (_playerInput == null)
        {
            _playerInput = FindObjectOfType<PlayerInput>();
        }
    }

    private bool _targeting;

    private void Update()
    {
      
    }
}
