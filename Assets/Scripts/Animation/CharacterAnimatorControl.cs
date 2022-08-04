using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightPhysics;


public class CharacterAnimatorControl : MonoBehaviour
{
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private Animator[] _animators;

    private void OnValidate()
    {
        if(_mover == null)
        {
            _mover = GetComponent<CharacterMover>();
        }
        if(_animators == null)
        {
            UpdateAnimatorArray();
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
            foreach (Animator animator in _animators)
            {
                animator.SetFloat("Speed", _mover.Velocity.magnitude);
            }
        }
    }

    private void OnMoveEnd()
    {
        foreach (Animator animator in _animators)
        {
            animator.SetFloat("Speed", 0);
        }
    }

    public void UpdateAnimatorArray()
    {
        _animators = GetComponentsInChildren<Animator>();
    }
}
