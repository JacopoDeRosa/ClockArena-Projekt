using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System;

public class CharacterMover : MonoBehaviour,  ISleeper
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private NavMeshObstacle _obstacle;
    [SerializeField] private float _stoppingDistance;
    [FoldoutGroup("Events")]
    public UnityEvent onMoveStart;
    [FoldoutGroup("Events")]
    public UnityEvent onMoveEnd;

    private bool _moving;
    private Vector3 _targetPosition;

    private bool _shouldToggleNavmesh;
    private bool _waitedForNavmeshUpdate = true;

    private bool IsAtTarget { get => Vector3.Distance(transform.position, _targetPosition) <= _stoppingDistance; }
    public bool IsMoving { get => _moving; }

    private void Awake()
    {
        _waitedForNavmeshUpdate = true;
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
        if(_shouldToggleNavmesh)
        {
            if (_waitedForNavmeshUpdate)
            {
                _waitedForNavmeshUpdate = false;
            }
            else
            {
                _shouldToggleNavmesh = false;
                _agent.enabled = true;
                _waitedForNavmeshUpdate = true;
            }
        }

        if (_moving)
        {
            if (IsAtTarget)
            {
                _moving = false;
                onMoveEnd.Invoke();
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
        /*
        _obstacle.enabled = false;
        _shouldToggleNavmesh = true;
        */
        StartCoroutine(OnWakeUp());
   
    }

    private IEnumerator OnWakeUp()
    {
        _obstacle.enabled = false;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        _agent.enabled = true;
    }

}
