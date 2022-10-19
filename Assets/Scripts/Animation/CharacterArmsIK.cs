using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterArmsIK : MonoBehaviour
{
    [SerializeField] private Rig _ikArmsRig;
    [SerializeField] private bool _ikToggle;
    [SerializeField] private TwoBoneIKConstraint _leftArmConstraint, _rightArmConstraint;
    [SerializeField] [Range(0, 1)] private float _leftArmIk, _rightArmIk;

    private Transform _leftArmControl, _leftArmHint;
    private Transform _rightArmControl, _rightArmHint;


    public void SetLeftArm(Transform control, Transform hint)
    {
        _leftArmControl = control;
        _leftArmHint = hint;
    }

    public void SetRightArm(Transform control, Transform hint)
    {
        _rightArmControl = control;
        _rightArmHint = hint;
    }

    public void SetIkToggle(bool toggle)
    {
        _ikToggle = toggle;
    }

    private void LateUpdate()
    {
        if(_ikToggle)
        {
            _ikArmsRig.weight = 1;
        }
        else
        {
            _ikArmsRig.weight = 0;
        }

        _leftArmConstraint.weight = _leftArmIk;
        _rightArmConstraint.weight = _rightArmIk;

        if(_rightArmControl != null && _rightArmHint != null)
        {
            _rightArmConstraint.data.target.position = _rightArmControl.position;
            _rightArmConstraint.data.target.rotation = _rightArmControl.rotation;
            _rightArmConstraint.data.hint.position = _rightArmHint.position;
            _rightArmConstraint.data.hint.rotation = _rightArmHint.rotation;
        }

        if (_leftArmControl != null && _leftArmHint != null)
        {
            _leftArmConstraint.data.target.position = _leftArmControl.position;
            _leftArmConstraint.data.target.rotation = _leftArmControl.rotation;
            _leftArmConstraint.data.hint.position = _leftArmHint.position;
            _leftArmConstraint.data.hint.rotation = _leftArmHint.rotation;
        }
    }


}
