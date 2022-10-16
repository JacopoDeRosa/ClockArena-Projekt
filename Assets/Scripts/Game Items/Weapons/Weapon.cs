using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameItem<WeaponData>
{
    [SerializeField] private AnimatorOverrideController _animatorOverride;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Vector3 _rotationOffset;
    [SerializeField] private Vector3 _chestAimOffset;
    [SerializeField] private Transform _leftHandPosition;
    [SerializeField] private Transform _leftHandHint;


    public AnimatorOverrideController AnimatorOverride { get => _animatorOverride; }
    public Vector3 PositionOffset { get => _positionOffset; }
    public Vector3 RotationOffset { get => _rotationOffset; }
    public Vector3 ChestAimOffset { get => _chestAimOffset; }

    public Transform LeftHandPosition { get => _leftHandPosition; }
    public Transform LeftHandHint { get => _leftHandHint; }
    

    public virtual void Attack()
    {

    }

}


