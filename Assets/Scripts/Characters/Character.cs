using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class Character : MonoBehaviour
{   
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Faction _faction;
    [SerializeField] private bool _isPlayerCharacter;
    [SerializeField] private CharacterMover _characterMover;
    [SerializeField] private Equipment _characterEquipment;
    [SerializeField] private CharacterDataReader _dataReader;
    [SerializeField] private CharacterStats _stats;
    [SerializeField] private CharacterAnimatorControl _animator;
    [SerializeField] private CharacterAbilities _abilities;
    [SerializeField] private CharacterVoice _voice;
    [SerializeField] private CharacterGUI _gui;
    [SerializeField] private RigSocketsControl _rigSockets;
    [SerializeField] private CharacterArmsIK _characterArmsIK;
    [SerializeField] private CharacterAimController _aimController;
    [SerializeField] private int _exp = 0;
    [SerializeField] private int _level = 1;

    [ShowInInspector][ReadOnly]
    private bool _sleep;

    [ShowInInspector][ReadOnly]
    private int _initiativeLevel = 0;

    public string Name { get => _name; }
    public Sprite Icon { get => _icon; }
    public Faction Faction { get => _faction; }
    public bool IsPlayerCharacter { get => _isPlayerCharacter; }
    public CharacterMover Mover { get => _characterMover; }
    public Equipment Equipment { get => _characterEquipment; }
    public CharacterDataReader DataReader { get => _dataReader; }
    public CharacterStats Stats { get => _stats; }
    public CharacterAnimatorControl Animator { get => _animator; }
    public CharacterAbilities Abilities { get => _abilities; }
    public CharacterVoice Voice { get => _voice; }
    public CharacterGUI GUI { get => _gui; }
    public RigSocketsControl RigSockets { get => _rigSockets; }
    public CharacterArmsIK ArmsIK { get => _characterArmsIK; }
    public CharacterAimController AimController { get => _aimController; }
    public int InitiativeLevel { get => _initiativeLevel; }
    public int Level { get => _level; }
    public int Exp { get => _exp; }
    public int ExpToNextLevel { get => 1000 + (400 * (_level - 1)); }


    public event Action onTurnStarted;

  
    public int RollInitiative()
    {
        _initiativeLevel = UnityEngine.Random.Range(0, TurnInitiativeCalculator.MaxInitiative + 1);
        return _initiativeLevel;
    }

    public void SetSleepState(bool state)
    {
        _sleep = state;
        if(_sleep)
        {
            GoToSleep();
        }
        else
        {
            WakeUp();
        }
    }
    public void StartTurn()
    {
        onTurnStarted?.Invoke();
    }

    private void GoToSleep()
    {
        foreach (var sleeper in GetComponents<ISleeper>())
        {
            sleeper.Sleep();
        }
    }
    private void WakeUp()
    {
        foreach (var sleeper in GetComponents<ISleeper>())
        {
            sleeper.WakeUp();
        }
    }

    public void SetName(string name)
    {
        _name = name;
    }
    public void SetIcon(int icon)
    {
        _icon = GameItemDB.GetDbOfType<IconsDB>().GetItem(icon);
        _gui.SetImage(_icon);
    }
  
    public void SetFaction(Faction faction)
    {
        _faction = faction;
    }
    public void SetExp(int exp)
    {
        _exp = exp;
    }
    public void SetLevel(int level)
    {
        _level = level;
    }
    public void SetIsPlayer(bool isPlayer)
    {
        _isPlayerCharacter = isPlayer;
        GUI.SetGuiFriendly(isPlayer);
    }
}

