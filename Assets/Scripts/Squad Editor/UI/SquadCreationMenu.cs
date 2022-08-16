using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SquadCreationMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private SquadEditor _editor;


    private Faction _selectedFaction;

    private void Start()
    {
        ChangeSelectedFaction(0);
    }

    public void ChangeSelectedFaction(int faction)
    {
        if(faction == 0)
        {
            _selectedFaction = Faction.DeathRow;
        }
        else if(faction == 1)
        {
            _selectedFaction = Faction.Slummers;
        }
        else if(faction == 2)
        {
            _selectedFaction = Faction.Traitors;
        }
        ChangeButtonsEnabled(faction);
    }
    private void ChangeButtonsEnabled(int disabledButton)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            Button button = _buttons[i];
            if (i == disabledButton)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }
        _buttons[disabledButton].interactable = false;
    }

    public void CreateSelectedSquad()
    {
        if (string.IsNullOrEmpty(_nameField.text) || string.IsNullOrWhiteSpace(_nameField.text)) return;

        _editor.CreateNewSquad(_selectedFaction, _nameField.text);
        gameObject.SetActive(false);
    }
}

