using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    [SerializeField]
    private ActionBarButton[] _allButtons;
    [SerializeField]
    private ActionsScheduler _actionsScheduler;

    private Queue<ActionBarButton> _freeButtons;

    private void Awake()
    {
        _freeButtons = new Queue<ActionBarButton>(_allButtons);
        _actionsScheduler.onActionAdded += AddActionButton;
    }

    private void AddActionButton(CharacterAction action)
    {
        var button = _freeButtons.Dequeue();
        button.gameObject.SetActive(true);
        button.Init(action);
    }

    
}
