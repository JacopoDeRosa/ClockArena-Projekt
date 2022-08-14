using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSystem : MonoBehaviour
{
    [SerializeField] private List<Gear> _gears = new List<Gear>();
    [Tooltip("Speed is in Degrees Per Second")]
    [SerializeField] private float _speed;
    [SerializeField] private bool _stopped;

    private void Update()
    {
        if (_stopped) return;
        foreach (Gear gear in _gears)
        {
            gear.Rotate(_speed * Time.deltaTime);
        }
    }


    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
