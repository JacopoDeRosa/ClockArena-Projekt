using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAimController : MonoBehaviour
{
    [SerializeField] private Rig _aimRig;
    [SerializeField] private MultiAimConstraint _chestAim;
    [SerializeField] private Transform _aimPosition;

    private Vector3 _planarPosition;
    private Vector3 _aimPlanarPosition;

    public Vector3 AimPointPosition { get => _aimPosition.position; }

    private void Awake()
    {
        _planarPosition = new Vector3(transform.position.x, 0, transform.position.z);
        _aimPlanarPosition = new Vector3(_aimPosition.position.x, 0, _aimPosition.position.z);
    }

    public void StartAiming()
    {
        StartCoroutine(StartAimRoutine());
    }

    public void StopAiming()
    {

        StartCoroutine(StopAimRoutine());
    }

    private IEnumerator StartAimRoutine()
    {

        while(_aimRig.weight < 1)
        {
            _aimRig.weight += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        _aimRig.weight = 1;
    }
    private IEnumerator StopAimRoutine()
    {
        while (_aimRig.weight > 0)
        {
            _aimRig.weight -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        _aimRig.weight = 0;
    }

    public void SetAimPointPosition(Vector3 position)
    {
   //   _planarPosition.Set(transform.position.x, 0, transform.position.z);
        _aimPosition.position = position;
    }

    public void SetAimOffset(Vector3 offset)
    {
        _chestAim.data.offset = offset;
    }
}
