using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEndAction : MonoBehaviour, IBarAction
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private CharacterVoice _voice;

    public event Action onActionEnd;

    private GameTurnManager _turnManager;

    private void Awake()
    {
        _turnManager = FindObjectOfType<GameTurnManager>();
    }

    private void EndTurn()
    {
        _turnManager.SetNextCharacter();
        _voice.PlayAcknowledge();
        onActionEnd?.Invoke();
    }

    private void Cancel()
    {

    }

    public IEnumerable<BarAction> GetBarActions()
    {
        yield return new BarAction(EndTurn, Cancel, this, "End Turn", "End this character's turn", _icon);
    }
}
