using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAudioCue : MonoBehaviour
{
    [SerializeField] private CharacterAction _target; 
    [SerializeField] protected AudioClip[] _cues;
    [SerializeField] protected AudioSource _audioSource;


    private void Awake()
    {
        _target.performed += OnAction;
    }
    private void OnDestroy()
    {
        if(_target != null)
        {
            _target.performed -= OnAction;
        }
    }

    protected virtual void OnAction(object[] args)
    {
        _audioSource.PlayOneShot(_cues[Random.Range(0, _cues.Length)]);
    }

}
