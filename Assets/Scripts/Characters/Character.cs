using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Character : MonoBehaviour
{   
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Faction _faction;
    [SerializeField] private CharacterMover _characterMover;
    [SerializeField] private Equipment _characterEquipment;
    [SerializeField] private CharacterDataReader _dataReader;
    [SerializeField] private CharacterStats _stats;
    [SerializeField] private CharacterAnimatorControl _animator;
    [SerializeField] private CharacterAbilities _abilities;
    [SerializeField] private int _exp = 0;
    [SerializeField] private int _level = 1;

    [ShowInInspector][ReadOnly]
    private bool _sleep;

    [ShowInInspector][ReadOnly]
    private int _initiativeLevel = 0;

    public string Name { get => _name; }
    public Sprite Icon { get => _icon; }
    public Faction Faction { get => _faction; }
    public CharacterMover Mover { get => _characterMover; }
    public Equipment Equipment { get => _characterEquipment; }
    public CharacterDataReader DataReader { get => _dataReader; }
    public CharacterStats Stats { get => _stats; }
    public CharacterAnimatorControl Animator { get => _animator; }
    public CharacterAbilities Abilities { get => _abilities; }
    public int InitiativeLevel { get => _initiativeLevel; }
    public int Level { get => _level; }
    public int Exp { get => _exp; }
    public int ExpToNextLevel { get => 1000 + (400 * (_level - 1)); }

  
    public int RollInitiative()
    {
        _initiativeLevel = Random.Range(0, TurnInitiativeCalculator.MaxInitiative + 1);
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
        _icon = GameItemDB.GetDbOfType<IconsDB>(IconsDB.Name).GetItem(icon);
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
}

