using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSingleton : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;

    public static PlayerInput Instance { get; private set; }

    private void Awake()
    {
        if(FindObjectsOfType<PlayerInputSingleton>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = _input;
            DontDestroyOnLoad(gameObject);
        }
    }
}
