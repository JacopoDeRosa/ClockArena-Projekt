using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFX : MonoBehaviour
{
    [SerializeField] private float _duration;

    private void Awake()
    {
        Destroy(gameObject, _duration);
    }
}
