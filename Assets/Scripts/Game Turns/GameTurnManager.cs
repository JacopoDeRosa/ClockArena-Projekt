using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameTurnManager : MonoBehaviour
{
    [SerializeField]
    private List<Character> _playingCharacters;
    [SerializeField] private TurnInitiativeCalculator _initiveHandler;
   
    /// <summary>
    /// Returns the number of the turn
    /// </summary>
    public UnityEvent<int> onTurnStarted;
    public UnityEvent<Character> onNextCharacter;
    
    private int _turnNumber = 0;
    private Character _activeCharacter;

    public List<Character> PlayingCharacters { get => _playingCharacters; }
    public Character ActiveCharacter { get => _activeCharacter; }

    public void AddCharacters(List<Character> characters)
    {
        foreach (Character character in characters)
        {
            AddCharacter(character);
        }
       
    }
    public void AddCharacter(Character character)
    {
        _playingCharacters.Add(character);
        character.Stats.onDeath += RemoveCharacter;
    }
    private void RemoveCharacter(Character character)
    {
        _playingCharacters.Remove(character);
    }

    public void BeginNewTurn()
    {
        _turnNumber++;
        onTurnStarted.Invoke(_turnNumber);
        _initiveHandler.RollTurnInitiave(PlayingCharacters);
        SetNextCharacter();
    }

    public void SetNextCharacter()
    {
        if (_activeCharacter != null)
        {
            _activeCharacter.SetSleepState(true);
        }
        _activeCharacter = _initiveHandler.GetNextCharacter();

        // If there are no more characters in the queue the active character will be null
        if(_activeCharacter == null)
        {
            BeginNewTurn();
            return;
        }

        onNextCharacter.Invoke(_activeCharacter);
        _activeCharacter.SetSleepState(false);
        _activeCharacter.StartTurn();
    }
}
