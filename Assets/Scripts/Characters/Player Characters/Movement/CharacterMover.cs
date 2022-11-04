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
    [SerializeField] private Character _user;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private NavMeshObstacle _obstacle;
    [SerializeField] private float _stoppingDistance;

    [FoldoutGroup("Events")]
    public UnityEvent onMoveStart;
    [FoldoutGroup("Events")]
    public UnityEvent onMoveEnd;

    private MousePointGetter _mousePointGetter;
    private WorldGizmos _worldGizmos;

    private Stance _stance;
    private bool _moving;

    private bool _targeting;
    private PlayerInput _input;
    private IconsDB _iconsDB;

    public event Action onActionEnd;
    public event Action<Stance> onStanceChange;
    
    private bool IsAtTarget { get => _agent.remainingDistance <= _stoppingDistance; }
    public bool IsMoving { get => _moving; }
    public Vector3 Velocity { get => _agent.velocity; }
    public Stance CurrentStance { get => _stance; }

    private void Awake()
    {
        _mousePointGetter = FindObjectOfType<MousePointGetter>();
        _worldGizmos = FindObjectOfType<WorldGizmos>();
        _iconsDB = GameItemDB.GetDbOfType<IconsDB>();
    }

    private void Start()
    {
        _input = PlayerInputSingleton.Instance;
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
            if (_mousePointGetter.GetMousePoint(out Vector3 point) && TryCalculatePath(point, out Vector3[] points, out int lenght) && _user.Stats.CheckStatsAvailable(lenght * 2, lenght * 5))
            {
                _user.Stats.SpendStats(lenght * 2, lenght * 5);
                MoveToPoint(point);
                _user.Voice.PlayMoving();
            }
            _targeting = false;
            _worldGizmos.ClearNavPath();
            _worldGizmos.ResetPointer();
            
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

    public IEnumerator MoveToPointRoutine(Vector3 point)
    {
        MoveToPoint(point);
        while(_moving)
        {
            yield return null;
        }
    }

    public bool TryCalculatePath(Vector3 position)
    {
        NavMeshPath path = new NavMeshPath();
        _agent.CalculatePath(position, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }

    public bool TryCalculatePath(Vector3 position, out Vector3[] pathPoints, out int length)
    {
        pathPoints = new Vector3[0];
        length = 0;

        NavMeshPath path = new NavMeshPath();
        _agent.CalculatePath(position, path);
        bool pathComplete = path.status == NavMeshPathStatus.PathComplete;

        if (pathComplete)
        {
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                Vector3 point = path.corners[i];
                Vector3 nextPoint = path.corners[i + 1];

                length += (int) Vector3.Distance(point, nextPoint);
            }

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
                bool validPath = TryCalculatePath(mousePosition, out Vector3[] points, out int lenght) && _user.Stats.CheckStatsAvailable(lenght * 2, lenght * 5);
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
        if (_stance == Stance.Prone)
        {
            onActionEnd?.Invoke();
        }
        else
        {
            _targeting = true;
        }
    }

    private bool CancelTargeting()
    {
        if (_moving) return false;

        _worldGizmos.ClearNavPath();
        _worldGizmos.ResetPointer();
        _targeting = false;
        return true;
    }

    private void ToggleCrouch()
    {
        if(_stance == Stance.Prone)
        {
            _user.Animator.SetBool("Crouched", false);
            _stance = Stance.Standing;
        }
        else if(_stance == Stance.Standing)
        {
            _user.Animator.SetBool("Crouched", true);
            _stance = Stance.Prone;
        }
        onStanceChange?.Invoke(_stance);
        StartCoroutine(EndActionDelayed(1));
    }

    private bool NoCancel()
    {
        return false;
    }

    private IEnumerator EndActionDelayed(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        onActionEnd?.Invoke();
    }

    public IEnumerable<BarAction> GetBarActions()
    {
        yield return new BarAction(StartTargeting, CancelTargeting, this, "Move", "Move to the selected position", _iconsDB.MoveSprite);
        yield return new BarAction(ToggleCrouch, NoCancel, this, "Crouch/Stand", "Have this character crouch or stand \n (Some actions won't work when crouched)", _iconsDB.CrouchSprite);
    }
}
