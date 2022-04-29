using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveCharacterMover : MonoBehaviour, IAction
{
    [SerializeField] private Sprite _actionSprite;
    [SerializeField] private GameTurnManager _turnManager;
    [SerializeField] private MousePointGetter _pointGetter;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private WorldGizmos _worldGizmos;
    [SerializeField] private Gradient _validPathColor, _invalidPathColor;

    private bool _targeting = false;
    [SerializeField]
    private LayerMask _movementMask;


    private ActionsScheduler _actionsScheduler;


    public event ActionEventHandler onBegin;
    public event Action onEnd;
    public event Action onCancel;

    private void OnValidate()
    {
        if (_playerInput == null)
        {
            _playerInput = FindObjectOfType<PlayerInput>();
        }
    }
    private void Awake()
    {
        _playerInput.actions["Confirm"].started += OnConfirmDown;
    }
    private void Start()
    {
        _actionsScheduler = FindObjectOfType<ActionsScheduler>();
        if (_actionsScheduler != null)
        {
            _actionsScheduler.AddAction(this);
        }
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
                if (_turnManager.ActiveCharacter.Mover.TryCalculatePath(hit, out Vector3[] points, out float lenght))
                {
                    _turnManager.ActiveCharacter.Mover.MoveToPoint(points[points.Length-1]);
                    _turnManager.ActiveCharacter.Mover.onMoveEnd.AddListener(InvokeOnMoveEnd);
                   
                }
                else
                {
                    onEnd?.Invoke();
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

    public void Begin()
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
    public bool Cancel()
    {
        if(_turnManager.ActiveCharacter.Mover.IsMoving)
        {
            return false;
        }
        _targeting = false;
        onCancel?.Invoke();
        _worldGizmos.ClearPath();
        _worldGizmos.ResetPointer();
        return true;
    }
    private void InvokeOnMoveEnd()
    {
        onEnd?.Invoke();
        _turnManager.ActiveCharacter.Mover.onMoveEnd.RemoveListener(InvokeOnMoveEnd);
    }
    public Sprite GetActionIcon()
    {
        return _actionSprite;
    }
}
