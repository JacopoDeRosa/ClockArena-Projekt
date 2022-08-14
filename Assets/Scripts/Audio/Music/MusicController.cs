using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private AudioSource _audioSource;


    private void Start()
    {
        PlayRandomClip();
    }
    private void Update()
    {
        if(_audioSource.isPlaying == false)
        {
            PlayRandomClip();
        }
    }
    private void PlayRandomClip()
    {
        _audioSource.clip = _clips[Random.Range(0, _clips.Length)];
        _audioSource.Play();
    }
}
