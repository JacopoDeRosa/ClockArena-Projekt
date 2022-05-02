using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class ActiveCharacterMover : PermanentAction
{
    [SerializeField] private GameTurnManager _turnManager;
    [SerializeField] private MousePointGetter _pointGetter;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private WorldGizmos _worldGizmos;
    [SerializeField] private Gradient _validPathColor, _invalidPathColor;

    private bool _targeting = false;

    protected override void OnValidate()
    {
        base.OnValidate();
        if (_playerInput == null)
        {
            _playerInput = FindObjectOfType<PlayerInput>();
        }
    }

    private void Awake()
    {
        _playerInput.actions["Confirm"].started += OnConfirmDown;
    }

    private void OnDestroy()
    {
        if (_playerInput != null)
        {
            _playerInput.actions["Confirm"].started -= OnConfirmDown;
        }
    }

    private void OnConfirmDown(InputAction.CallbackContext context)
    {
        if (_targeting)
        {
            _targeting = false;
            if (_pointGetter.GetMousePoint(out Vector3 hit))
            {
                Character activeCharacter = _turnManager.ActiveCharacter;
                CharacterMover activeCharacterMover = _turnManager.ActiveCharacter.Mover;

                if (activeCharacterMover.TryCalculatePath(hit, out Vector3[] points, out float lenght))
                {
                    activeCharacterMover.MoveToPoint(points.Last());
                    activeCharacterMover.onMoveEnd.AddListener(OnMoveEnd);
                    performed?.Invoke(activeCharacter);
                }
                else
                {
                    End();
                }
            }
           
            _worldGizmos.ClearPath();
            _worldGizmos.ResetPointer();
        }
    }

    private void Update()
    {
        if (_targeting)
        {
            if (_pointGetter.GetMousePoint(out Vector3 hit))
            {
                if (_turnManager.ActiveCharacter.Mover.TryCalculatePath(hit, out Vector3[] points, out float lenght))
                {
                    _worldGizmos.RenderPath(points, _validPathColor);                   
                }
                else
                {
                    _worldGizmos.SetPathColor(_invalidPathColor);
                }
                _worldGizmos.SetPointerPosition(hit);
            }
        }
    }

    public override void Begin()
    {
        if (_actionsScheduler.Busy) return;
        if (_targeting)
        {
            Cancel();
            return;
        }
        _targeting = true;
        onBegin?.Invoke(this);
    }
    public override bool Cancel()
    {
        if(_turnManager.ActiveCharacter.Mover.IsMoving)
        {
            return false;
        }
        _targeting = false;
        _worldGizmos.ClearPath();
        _worldGizmos.ResetPointer();
        return true;
    } 
    private void OnMoveEnd()
    {
        End();
        _turnManager.ActiveCharacter.Mover.onMoveEnd.RemoveListener(OnMoveEnd);
    }
}
