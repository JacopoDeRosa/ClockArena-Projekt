using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameTurnManager : MonoBehaviour
{
    [SerializeField][ReadOnly]
    private List<Character> _playingCharacters;
    [SerializeField] private TurnInitiativeCalculator _initiveHandler;
   
    /// <summary>
    /// Returns the number of the turn
    /// </summary>
    [FoldoutGroup("Events")]
    public UnityEvent<int> onTurnStarted;
    [FoldoutGroup("Events")]
    public UnityEvent<Character> onNextCharacter;

    [ShowInInspector][ReadOnly][FoldoutGroup("Info")]
    private int _turnNumber = 0;
    [ShowInInspector][ReadOnly][FoldoutGroup("Info")]
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
