using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilitiesWindow : MonoBehaviour
{
    [SerializeField] private FoldingBar _foldingBar;
    [SerializeField] private SquadEditor _squadEditor;
    [SerializeField] private AbilityTierUI[] _tierSlots;

    private AbilityTree _activeAbilityTree;
    private Character _activeCharacter;


    private void Awake()
    {
        _squadEditor.onFocus += OnFocus;
        _squadEditor.onLoseFocus += OnLoseFocus;
    }

    private void OnValidate()
    {
        if(_tierSlots.Length != 10)
        {
            _tierSlots = new AbilityTierUI[10];
        }
    }

    private void OnFocus(int index)
    {
        _foldingBar.Toggle(true);
        _activeCharacter = _squadEditor.GetCharacter(index);
        _activeAbilityTree = _activeCharacter.Abilities.ActiveTree;
        ReadActiveAbilityTree();
    }

    private void OnLoseFocus()
    {
        _foldingBar.Toggle(false);
    }

    private void ReadActiveAbilityTree()
    {

        for (int i = 0; i < 10; i++)
        {
            AbilityTier tier = _activeAbilityTree.GetAbilityTier(i);
            _tierSlots[i].ReadAbilityTier(tier);
            if(i > _activeCharacter.Level - 1)
            {
                _tierSlots[i].SetLocked(true);
            }
            else
            {
                _tierSlots[i].SetLocked(false);
            }
        }
    }
}
