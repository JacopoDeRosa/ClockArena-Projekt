using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterEquipmentWindow : MonoBehaviour
{
    [SerializeField] private FoldingBar _foldingBar;
    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private TMP_Text _levelText, _costText;
    [SerializeField] private Image _icon;
    [SerializeField] private SquadEditor _squadEditor;

    private CharacterComponentsData _activeCharacterData;
    private Character _activeCharacter;
    private int _activeCharacterIndex;
    private LoadingScreen _loadingScreen;
    

    private int _totalCost;

    public FoldingBar FoldingBar { get => _foldingBar; }

    private void Awake()
    {
        _loadingScreen = FindObjectOfType<LoadingScreen>(true);
        _nameField.onEndEdit.AddListener(SetCharacterName);
        _squadEditor.onFocus += OnFocus;
        _squadEditor.onLoseFocus += OnLoseFocus;
    }

    private void OnFocus(int index)
    {
        Character character = _squadEditor.GetCharacter(index);
        CharacterComponentsData data = _squadEditor.GetCharacterData(index);

        _foldingBar.Toggle(true);
        _nameField.text = character.Name;
        _levelText.text = "Level: " + character.Level.ToString() + " - EXP " + character.Exp.ToString().PadLeft(4,'0') + "/1000"; // TODO: Replace 1000 with the exp to next level
        _icon.sprite = character.Icon;
        _activeCharacter = character;
        _activeCharacterData = new CharacterComponentsData(data);
        _activeCharacterIndex = index;
        _totalCost = 0;
    }

    private void OnLoseFocus()
    {
        _foldingBar.Toggle(false);
    }

    public void SetCharacterName(string name)
    {
        if (_activeCharacterData == null) return;
        _activeCharacterData.name = name;
        _activeCharacter.SetName(name);
    }

    public void TryApplyChanges()
    {
        StartCoroutine(ApplyChangesRoutine());
    }

    private void ApplyChanges(bool available)
    {
        if(available)
        {
            _squadEditor.UpdateCharacterData(_activeCharacterIndex, _activeCharacterData);
        }
    }

    private IEnumerator ApplyChangesRoutine()
    {
        _loadingScreen.gameObject.SetActive(true);

        _loadingScreen.SetText("Checking with server");
        yield return NetworkUtility.TrySpendACoins(_totalCost, ApplyChanges);

        _loadingScreen.SetText("Updating Squad");
        yield return _squadEditor.UpdateSquad();

        _loadingScreen.gameObject.SetActive(false);
    }

}
