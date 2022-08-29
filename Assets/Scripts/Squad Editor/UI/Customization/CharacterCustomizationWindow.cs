using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterCustomizationWindow : MonoBehaviour
{
    [SerializeField] private FoldingBar _foldingBar;

    [SerializeField] private TMP_InputField _nameField;

    [SerializeField] private TMP_Text _levelText, _costText;

    [SerializeField] private Image _icon;

    [SerializeField] private SquadEditor _squadEditor;

    [SerializeField] private ItemWindow _itemWindow;
    [SerializeField] private ItemUiSlot _headSlot, _bodySlot, _weaponSlot, _gadgetSlot;

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

        _headSlot.onClear += ClearHeadSlot;
        _bodySlot.onClear += ClearBodySlot;

        _headSlot.onClick += DisplayHeadArmour;
        _bodySlot.onClick += DisplayBodyArmour;
    }

    private void OnFocus(int index)
    {
        Character character = _squadEditor.GetCharacter(index);
        CharacterComponentsData data = _squadEditor.GetCharacterData(index);

        _foldingBar.Toggle(true);
        ReadCharacter(character);

        _activeCharacterData = new CharacterComponentsData(data);
        _activeCharacterIndex = index;
        _totalCost = 0;
        ReadCharacterEquipment(_activeCharacter);
    }

    private void OnLoseFocus()
    {
        _foldingBar.Toggle(false);
    }

    private void ReadCharacter(Character character)
    {
        _nameField.text = character.Name;
        _levelText.text = "Level: " + character.Level.ToString() + " - EXP " + character.Exp.ToString().PadLeft(4, '0') + "/" + character.ExpToNextLevel.ToString(); // TODO: Replace 1000 with the exp to next level
        _icon.sprite = character.Icon;
        _activeCharacter = character;
    }

    private void ReadCharacterEquipment(Character character)
    {
        _headSlot.SetItem(character.Equipment.HeadArmour);
        _bodySlot.SetItem(character.Equipment.BodyArmour);
        _weaponSlot.SetItem(character.Equipment.Weapon);
        _gadgetSlot.SetItem(character.Equipment.Gadget);
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

    private IEnumerator ApplyChangesRoutine()
    {
        _loadingScreen.gameObject.SetActive(true);

        _loadingScreen.SetText("Checking with server");

        void ApplyChanges(bool available)
        {
            if (available)
            {
                _squadEditor.UpdateCharacterData(_activeCharacterIndex, _activeCharacterData);
            }
        }

        int costSign = System.Math.Sign(_totalCost);

        if (costSign < 0)
        {
            yield return NetworkUtility.AddACoins(Mathf.Abs(_totalCost));
            ApplyChanges(true);
        }
        else if (costSign > 0)
        {
            yield return NetworkUtility.TrySpendACoins(_totalCost, ApplyChanges);
        }
        else
        {
            ApplyChanges(true);
        }

        _loadingScreen.SetText("Updating Squad");
        yield return _squadEditor.UpdateSquad();

        _loadingScreen.gameObject.SetActive(false);

        _squadEditor.LooseFocus();
    }

    private void UpdateCost(int change)
    {
        _totalCost += change;
        int cost = Mathf.Abs(_totalCost);
        int sign = System.Math.Sign(_totalCost);
        if(sign < 0)
        {
            _costText.text = "-" + cost.ToString().PadLeft(5, '0');
        }
        else
        {
            _costText.text = cost.ToString().PadLeft(5, '0');
        }
        
    }

    #region Item Filters
    private IEnumerable<ItemDescriptor> GetAvailableArmour(Character character, ArmourTypes armourType)
    {
        ArmourDB armourDB = GameItemDB.GetDbOfType<ArmourDB>();

        for (int i = 0; i < armourDB.Items.Length; i++)
        {
            Armour armour = armourDB.Items[i];

            if (armour == null || armour.Data == null) continue;

            if (ItemUsableByCharacter(armour, character) && armour.Data.ArmourType == armourType)
            {
                yield return new ItemDescriptor(i, armour.Data.Sprite, "Cost: " + armour.Data.Cost.ToString());
            }
        }
    }

    private bool ItemUsableByCharacter(GameItem item, Character character)
    {
        return item.Data.UsableByFaction(character.Faction) && item.Data.RequiredLevel <= character.Level;
    }
    #endregion

    #region Clear Equipment Functions
    private void ClearHeadSlot()
    {
        ClearHeadSlot(true);
    }
    private void ClearBodySlot()
    {
        ClearBodySlot(true);
    }
    private void ClearHeadSlot(bool recalculateMesh)
    {
        ItemData data = _activeCharacter.Equipment.ClearHeadArmour(recalculateMesh);
        _activeCharacterData.head = 0;
        _headSlot.SetItem(null);
        if (data != null) UpdateCost(-data.Cost);
    }
    private void ClearBodySlot(bool recalculateMesh)
    {
        ItemData data = _activeCharacter.Equipment.ClearBodyArmour(recalculateMesh);
        _activeCharacterData.body = 0;
        _bodySlot.SetItem(null);
        if(data != null) UpdateCost(-data.Cost);
    }
    private void ClearWeaponSlot()
    {
        ItemData data = _activeCharacter.Equipment.ClearWeapon();
        if (data != null) UpdateCost(-data.Cost);
    }
    private void ClearGadgetSlot()
    {
        ItemData data = _activeCharacter.Equipment.ClearGadget();
        if (data != null) UpdateCost(-data.Cost);
    }
    #endregion

    #region Set Equipment Functions
    private void SetHeadSlot(int item)
    {
        if(_activeCharacter.Equipment.HasHeadArmour)
        {
            ClearHeadSlot(false);
        }
        _activeCharacterData.head = item;
        Armour armour = _activeCharacter.Equipment.SetHeadArmour(item);

        _headSlot.SetItem(armour);
        UpdateCost(armour.Data.Cost);
    }
    private void SetBodySlot(int item)
    {
        if(_activeCharacter.Equipment.HasBodyArmour)
        {
            ClearBodySlot(false);
        }
        _activeCharacterData.body = item;
        Armour armour = _activeCharacter.Equipment.SetBodyArmour(item);

#if UNITY_EDITOR
        if(armour == null)
        {
            Debug.LogError("Armour passed in this method should not be null");
        }
#endif

        _bodySlot.SetItem(armour);
        UpdateCost(armour.Data.Cost);
    }

    //TODO: Add weapons and gadgets
    #endregion

    #region OpenItemWindowMethods
    public void DisplayHeadArmour()
    {
        _itemWindow.gameObject.SetActive(true);
        _itemWindow.transform.position.Set(0, 0, 0);
        _itemWindow.SetItemsSize(100, 100);
        _itemWindow.SetTitle("Head Armour");
        _itemWindow.ShowItems(GetAvailableArmour(_activeCharacter, ArmourTypes.Head));
        _itemWindow.SetCallback(SetHeadSlot);
    }
    public void DisplayBodyArmour()
    {
        _itemWindow.gameObject.SetActive(true);
        _itemWindow.transform.position.Set(0, 0, 0);
        _itemWindow.SetItemsSize(100, 200);
        _itemWindow.SetTitle("Body Armour");
        _itemWindow.ShowItems(GetAvailableArmour(_activeCharacter, ArmourTypes.Body));
        _itemWindow.SetCallback(SetBodySlot);
    }


    #endregion

}
