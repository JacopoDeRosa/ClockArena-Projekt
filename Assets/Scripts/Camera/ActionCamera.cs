using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActionCamera : MonoBehaviour
{
    [SerializeField] private PlayableDirector _cinematic;


    public void StartActionCamera(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        _cinematic.Play();
    }
}
