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
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private NavMeshObstacle _obstacle;
    [SerializeField] private float _stoppingDistance;

    [FoldoutGroup("Events")]
    public UnityEvent onMoveStart;
    [FoldoutGroup("Events")]
    public UnityEvent onMoveEnd;

    private bool _moving;
    private Vector3 _targetPosition;

    private bool _targeting;
    private PlayerInput _input;

    public event Action onActionEnd;

    private bool IsAtTarget { get => _agent.remainingDistance <= _stoppingDistance; }
    public bool IsMoving { get => _moving; }
    public Vector3 Velocity { get => _agent.velocity; }

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

    }


    public void MoveToPoint(Vector3 point)
    {
        if (_moving) return;
        _agent.isStopped = false;

        _agent.SetDestination(point);
        _targetPosition = point;
        _moving = true;
        _targetPosition = point;
        onMoveStart.Invoke();
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
        if (_moving)
        {
            if (IsAtTarget)
            {
                _moving = false;
                onMoveEnd.Invoke();
                onActionEnd?.Invoke();
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
        Debug.Log("Targeting");
    }

    private void CancelTargeting()
    {

    }

    public IEnumerable<BarAction> GetBarActions()
    {
        // TODO: Add Generic action icons to the icons DB
        yield return new BarAction(StartTargeting, CancelTargeting, this, "Move", "Move to the selected position", _moveSprite);
    }
}
