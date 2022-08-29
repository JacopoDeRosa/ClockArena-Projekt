using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System;
using UnityEngine.InputSystem;

public class CharacterMover : MonoBehaviour,  ISleeper, IBarAction
{
    [SerializeField] private Sprite _moveSprite;
    [SerializeField] private CharacterVoice _voice;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private NavMeshObstacle _obstacle;
    [SerializeField] private float _stoppingDistance;

    [FoldoutGroup("Events")]
    public UnityEvent onMoveStart;
    [FoldoutGroup("Events")]
    public UnityEvent onMoveEnd;

    private MousePointGetter _mousePointGetter;
    private WorldGizmos _worldGizmos;

    private bool _moving;

    private bool _targeting;
    private PlayerInput _input;

    public event Action onActionEnd;

    private bool IsAtTarget { get => _agent.remainingDistance <= _stoppingDistance; }
    public bool IsMoving { get => _moving; }
    public Vector3 Velocity { get => _agent.velocity; }

    private void Awake()
    {
        _mousePointGetter = FindObjectOfType<MousePointGetter>();
        _worldGizmos = FindObjectOfType<WorldGizmos>();
    }

    private void Start()
    {
        _input = FindObjectOfType<PlayerInput>();
        if(_input)
        {
            _input.actions["Confirm"].started += OnConfirm;
        }
    }

    private void OnDestroy()
    {
        if (_input)
        {
            _input.actions["Confirm"].started -= OnConfirm;
        }
    }

    private void OnConfirm(InputAction.CallbackContext context)
    {   
        if(_targeting)
        {
            if (_mousePointGetter.GetMousePoint(out Vector3 point) && TryCalculatePath(point))
            {
                MoveToPoint(point);
            }
            _targeting = false;
            _worldGizmos.ClearNavPath();
            _worldGizmos.ResetPointer();
            _voice.PlayMoving();
        }
    }


    public void MoveToPoint(Vector3 point)
    {
        if (_moving) return;
        _agent.isStopped = false;
        NavMesh.SamplePosition(point, out NavMeshHit hit, _agent.height * 2, NavMesh.AllAreas);
        _agent.SetDestination(hit.position);
        _moving = true;
        onMoveStart.Invoke();
    }
    public bool TryCalculatePath(Vector3 position)
    {
        NavMeshPath path = new NavMeshPath();
        _agent.CalculatePath(position, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }
    public bool TryCalculatePath(Vector3 position, out Vector3[] pathPoints, out float length)
    {
        pathPoints = new Vector3[0];
        length = 0;

        NavMeshPath path = new NavMeshPath();
        _agent.CalculatePath(position, path);
        bool pathComplete = path.status == NavMeshPathStatus.PathComplete;

        if (pathComplete)
        {
            //TODO: Implement Distance
            pathPoints = path.corners;
        }
        return pathComplete;
    }
    private void Update()
    {
        if (_moving && _agent.pathPending == false)
        {
            if (IsAtTarget)
            {
                _moving = false;
                onMoveEnd.Invoke();
                onActionEnd?.Invoke();
            }
        }

        if(_targeting)
        {
            if (_mousePointGetter.GetMousePoint(out Vector3 mousePosition))
            {
                _worldGizmos.SetPointerPosition(mousePosition);
                bool validPath = TryCalculatePath(mousePosition, out Vector3[] points, out float lenght);
                _worldGizmos.RenderNavPath(points, validPath);
            }
        }
    }

    public void Sleep()
    { 
        _agent.enabled = false;
        _obstacle.enabled = true;
    }
    public void WakeUp()
    {
        StartCoroutine(OnWakeUp());
    }
    private IEnumerator OnWakeUp()
    {
        _obstacle.enabled = false;
        yield return null;
        yield return null;
        _agent.enabled = true;
    }

    private void StartTargeting()
    { 
        _targeting = true;
    }
    private void CancelTargeting()
    {
        _worldGizmos.ClearNavPath();
        _worldGizmos.ResetPointer();
        _targeting = false;
    }

    public IEnumerable<BarAction> GetBarActions()
    {
        // TODO: Add Generic action icons to the icons DB
        yield return new BarAction(StartTargeting, CancelTargeting, this, "Move", "Move to the selected position", _moveSprite);
    }
}
