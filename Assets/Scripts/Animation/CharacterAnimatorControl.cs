using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightPhysics;


public class CharacterAnimatorControl : MonoBehaviour
{
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private Animator _animator;

    private RuntimeAnimatorController _defaultController;

    private void OnValidate()
    {
        if (_mover == null)
        {
            _mover = GetComponent<CharacterMover>();
        }
    }

    private void Awake()
    {
        _defaultController = _animator.runtimeAnimatorController;
        _mover.onMoveEnd.AddListener(OnMoveEnd);
    }

    private void Update()
    {
        if (_mover.IsMoving)
        {
            _animator.SetFloat("Speed", _mover.Velocity.magnitude);
        }
    }

    private void OnMoveEnd()
    {
        _animator.SetFloat("Speed", 0);
    }

    public void SetBool(string name, bool value)
    {

        _animator.SetBool(name, value);

    }

    public void SetTrigger(string name)
    {

        _animator.SetTrigger(name);
    }

    public void ResetAnimatorOverride()
    {
        _animator.runtimeAnimatorController = _defaultController;
    }

    public void SetAnimatorOverride(AnimatorOverrideController overrideController)
    {
        _animator.runtimeAnimatorController = overrideController;
    }
}
