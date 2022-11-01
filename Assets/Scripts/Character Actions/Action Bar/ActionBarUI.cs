using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBarUI : MonoBehaviour
{
    [SerializeField]
    private ActionScheduler _actionScheduler;
    [SerializeField]
    private ActionBarButton[] _allButtons;

    private void Awake()
    {
        _actionScheduler.onActionsUpdated += OnActionsUpdate;
    }

    private void OnActionsUpdate(bool displayActions)
    {
        DiasbleAllButtons();
        if (displayActions)
        {
            for (int i = 0; i < _actionScheduler.ActiveActions.Count; i++)
            {
                BarAction barAction = _actionScheduler.ActiveActions[i];
                _allButtons[i].gameObject.SetActive(true);
                _allButtons[i].SetAction(barAction, i);
            }
        }
    }

    private void DiasbleAllButtons()
    {
        foreach (ActionBarButton button in _allButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

}
