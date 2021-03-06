using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Character : MonoBehaviour
{   
    [SerializeField] private string _name;
    [SerializeField] private Sprite _customIcon;
    [SerializeField] private CharacterData _data;
    [SerializeField] private CharacterMover _characterMover;
    [SerializeField] private Factions _faction;

    [ShowInInspector][ReadOnly]
    private bool _sleep;

    [ShowInInspector][ReadOnly]
    private int _initiativeLevel = 0;


   
    public string Name { get => _name; }
    public Sprite CustomIcon { get => _customIcon; }
    public CharacterData Data { get => _data; }
    public CharacterMover Mover { get => _characterMover; }
    public int InitiativeLevel { get => _initiativeLevel; }

  
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
}
