using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameItem<WeaponData>
{
    [SerializeField] private AnimatorOverrideController _animatorOverride;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Vector3 _rotationOffset;
    [SerializeField] private Vector3 _chestAimOffset;
    [SerializeField] private Socket _targetSocket = Socket.RightHand;
    [SerializeField] private bool _requireIK;
    [SerializeField] private Transform _leftHandPosition, _rightHandPosition;
    [SerializeField] private Transform _leftHandHint, _rightHandHint;
    [SerializeField] private float _attackTime;

    public AnimatorOverrideController AnimatorOverride { get => _animatorOverride; }
    public Vector3 PositionOffset { get => _positionOffset; }
    public Vector3 RotationOffset { get => _rotationOffset; }
    public Vector3 ChestAimOffset { get => _chestAimOffset; }

    public Socket TargetSocket { get => _targetSocket; }

    public Transform LeftHandPosition { get => _leftHandPosition; }
    public Transform LeftHandHint { get => _leftHandHint; }

    public Transform RightHandPosition { get => _rightHandPosition; }
    public Transform RightHandHint { get => _rightHandHint; }

    public bool RequireIk { get => _requireIK; }

    public float AttackTime { get => _attackTime; }
    

    public virtual void Attack()
    {

    }

}


