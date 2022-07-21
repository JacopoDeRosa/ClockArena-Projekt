using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Voice", menuName = "Audio/New Character Voice")]
public class CharacterVoice : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Factions _validFactions;
    [SerializeField] private AudioClip[] _acknowledge, _movement, _pain, _special, _attack, _reload, _outOfAmmo,_enemySpotted;

    public string Name { get => _name; }
    public Factions ValidFactions { get => _validFactions; }

    public AudioClip Acknowledge { get => GetRandomClip(_acknowledge); }
    public AudioClip Movement { get => GetRandomClip(_movement); }
    public AudioClip Pain { get => GetRandomClip(_pain); }
    public AudioClip Special { get => GetRandomClip(_special); }
    public AudioClip Attack { get => GetRandomClip(_attack); }
    public AudioClip Reload { get => GetRandomClip(_reload); }
    public AudioClip OutOfAmmo { get => GetRandomClip(_outOfAmmo); }
    public AudioClip EnemySpotted { get => GetRandomClip(_enemySpotted); }




    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        if (clips == null) return null;

        return clips[Random.Range(0, clips.Length)];
    }
}
