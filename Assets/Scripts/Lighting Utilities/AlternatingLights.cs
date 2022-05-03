using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatingLights : MonoBehaviour
{
    [SerializeField] private GameObject _light1, _light2;
    [SerializeField] private float _speed;

    private float _timer;

    private void Update()
    {
        _timer = Mathf.PingPong(Time.time * _speed, 1.5f);

        if(_timer >= 1)
        {
            _light1.SetActive(false);
            _light2.SetActive(true);
        }
        else if(_timer <= 0.5f)
        {
            _light1.SetActive(true);
            _light2.SetActive(false);
        }

    }
}
