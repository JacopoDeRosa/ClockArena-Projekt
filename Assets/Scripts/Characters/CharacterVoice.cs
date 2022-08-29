using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVoice : MonoBehaviour
{
    [SerializeField] private CharacterVoicePack _voicePack;
    [SerializeField] private AudioSource _audioSource;

    public void SetVoicePack(int pack)
    {
        _voicePack = GameItemDB.GetDbOfType<VoiceDB>(VoiceDB.Name).GetItem(pack);
    }

    public void PlayAcknowledge()
    {
        _audioSource.PlayOneShot(_voicePack.Acknowledge);
    }

    public void PlayMoving()
    {
        _audioSource.PlayOneShot(_voicePack.Movement);
    }
}
