using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilitiesWindow : MonoBehaviour
{
    [SerializeField] private FoldingBar _foldingBar;
    [SerializeField] private AbilityTree _abilityTree;
    [SerializeField] private SquadEditor _squadEditor;

    public FoldingBar FoldingBar { get => _foldingBar; }


    private void Awake()
    {
        _squadEditor.onFocus += OnFocus;
        _squadEditor.onLoseFocus += OnLoseFocus;
    }

    private void OnFocus(int index)
    {
        _foldingBar.Toggle(true);
    }

    private void OnLoseFocus()
    {
        _foldingBar.Toggle(false);
    }

}
