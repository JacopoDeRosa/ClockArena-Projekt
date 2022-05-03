using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightPhysics;


public class CharacterAnimatorControl : MonoBehaviour
{
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private KinematicVelocity _kineVelo;
    [SerializeField] private Animator _animator;

    private void OnValidate()
    {
        if(_mover == null)
        {
            _mover = GetComponent<CharacterMover>();
        }
        if(_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    private void Awake()
    {
        _mover.onMoveEnd.AddListener(OnMoveEnd);
    }

    private void Update()
    {
        if(_mover.IsMoving)
        {
            _animator.SetFloat("Speed", _mover.Velocity.magnitude);
         
        }
    }

    private void OnMoveEnd()
    {
        _animator.SetFloat("Speed", 0);
    }
}
