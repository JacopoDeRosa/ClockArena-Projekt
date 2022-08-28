using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class TurnInitiativeCalculator : MonoBehaviour
{
    public const int MaxInitiative = 12;

    public UnityEvent<Character[]> onInitiativeRoll;

    [ShowInInspector][ReadOnly]
    private Queue<Character> _initiativeOrder;
    [ShowInInspector][ReadOnly]
    private Character[] _charactersByInitiative;

    public Character[] CharactersByInitiative { get => _charactersByInitiative; }


    public void RollTurnInitiave(List<Character> characters)
    {
        foreach (Character character in characters)
        {
            character.RollInitiative();
        }

        _initiativeOrder = new Queue<Character>();
        for (int i = MaxInitiative; i >=  0; i--)
        {
            foreach(Character character in characters)
            {
                if(character.InitiativeLevel == i)
                {
                    _initiativeOrder.Enqueue(character);
                }
            }
        }
        _charactersByInitiative = _initiativeOrder.ToArray();
        onInitiativeRoll.Invoke(CharactersByInitiative);
    }

    public Character GetNextCharacter()
    {
        if(_initiativeOrder.Count > 0)
        {
            return _initiativeOrder.Dequeue();
        }

        return null;
    }
}
