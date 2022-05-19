using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicController : MonoBehaviour
{
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioClip[] _mainMenuClips;
    [SerializeField] private AudioClip[] _inGameClips;


    private IEnumerator ChangeClip(AudioClip targetClip)
    {
        yield return null;
    }
}
