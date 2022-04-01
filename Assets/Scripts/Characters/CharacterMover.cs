using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System;

public class CharacterMover : MonoBehaviour, IAction
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _stoppingDistance;
    [FoldoutGroup("Events")]
    public UnityEvent onMoveStart;
    [FoldoutGroup("Events")]
    public UnityEvent onMoveEnd;

    private bool _moving;
    private Vector3 _targetPosition;

    public event Action onActionStarted;
    public event Action onActionEnded;

    private bool IsAtTarget { get => Vector3.Distance(transform.position, _targetPosition) <= _stoppingDistance; }
    public bool IsMoving { get => _moving; }

    public void MoveToPoint(Vector3 point)
    {
        if (_moving) return;
        _agent.isStopped = false;

        _agent.SetDestination(point);
        _targetPosition = point;
        _moving = true;
        _targetPosition = point;
        onMoveStart.Invoke();
        onActionStarted?.Invoke();
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
                onActionEnded?.Invoke();
            }
        }
    }
}
