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

    private void Awake()
    {
        _planarPosition = new Vector3(transform.position.x, 0, transform.position.z);
        _aimPlanarPosition = new Vector3(_aimPosition.position.x, 0, _aimPosition.position.z);
    }

    public void StartAiming()
    {

    }

    public void StopAiming()
    {

    }

    private IEnumerator StartAimRoutine()
    {
        yield return null;
    }
    private IEnumerator StopAimRoutine()
    {
        yield return null;
    }

    public void SetAimPointPosition(Vector3 position)
    {
        _planarPosition.Set(transform.position.x, 0, transform.position.z);
    }

    public void SetAimOffset(Vector3 offset)
    {
        _chestAim.data.offset = offset;
    }
}
