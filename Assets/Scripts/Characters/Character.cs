using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Character : MonoBehaviour
{
    

    [SerializeField] private string _name;
    [SerializeField] private CharacterData _data;
    [SerializeField] private CharacterMover _characterMover;
    [SerializeField] private ActionScheduler _actionScheduler;

    [ShowInInspector][ReadOnly]
    private int _initiativeLevel = 0;


   
    public string Name { get => _name; }
    public CharacterData Data { get => _data; }
    public CharacterMover Mover { get => _characterMover; }
    public int InitiativeLevel { get => _initiativeLevel; }
  
    public int RollInitiative()
    {
        _initiativeLevel = Random.Range(0, TurnInitiative.MaxInitiative + 1);
        return _initiativeLevel;
    }



}
