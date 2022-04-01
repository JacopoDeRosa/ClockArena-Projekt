using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameTurnManager : MonoBehaviour
{
    [SerializeField] private List<Character> _playingCharacters;
    [SerializeField] private TurnInitiative _initiveHandler;
   

    
    /// <summary>
    /// Returns the number of the turn
    /// </summary>
    [FoldoutGroup("Events")]
    public UnityEvent<int> onTurnStarted;
    [FoldoutGroup("Events")]
    public UnityEvent onTurnEnded;

    [ShowInInspector][ReadOnly][FoldoutGroup("Info")]
    private int _turnNumber = 0;
    [ShowInInspector][ReadOnly][FoldoutGroup("Info")]
    private Character _activeCharacter;

    public List<Character> PlayingCharacters { get => _playingCharacters; }
    public Character ActiveCharacter { get => _activeCharacter; }


    public void BeginNewTurn()
    {
        _turnNumber++;
        onTurnStarted.Invoke(_turnNumber);
        _initiveHandler.RollTurnInitiave(PlayingCharacters);
        SetNextCharacter();
    }

    [Button]
    public void SetNextCharacter()
    {
       if(_activeCharacter != null) _activeCharacter.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
        _activeCharacter = _initiveHandler.GetNextCharacter();
        if(_activeCharacter == null)
        {
            BeginNewTurn();
            return;
        }
        _activeCharacter.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
    }

    private void Start()
    {
        BeginNewTurn();
    }
}
