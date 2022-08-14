using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadEditor : MonoBehaviour
{
    [SerializeField] private Character _characterTemplate;
    [SerializeField] private Transform[] _characterBases = new Transform[6];
    [SerializeField] private Character[] _characters = new Character[6];
    [SerializeField] private GameObject _inspectionCamera;
    [SerializeField] private CharacterAbilitiesWindow _abilitiesWindow;
    [SerializeField] private CharacterEquipmentWindow _equipmentWindow;
    [SerializeField] private Faction _squadFaction;

    private bool _focused;

    public void InitNewSquad()
    {

    }

    public Character GetCharacter(int index)
    {
        if(index >= _characters.Length)
        {
            return null;
        }
        else
        {
            return _characters[index];
        }
    }
    public Character SpawnCharacterAtIndex(int index)
    {
        if (index >= _characterBases.Length) return null;

        Character spawnedChar = Instantiate(_characterTemplate, _characterBases[index]);
        _characters[index] = spawnedChar;
        spawnedChar.GetComponent<CharacterVoice>().PlayAcknowledge();
        return spawnedChar;
    }
    public void FocusOnCharacter(int index)
    {
        if (_focused) return;
        _inspectionCamera.transform.rotation = Quaternion.identity;
        _inspectionCamera.transform.position = _characterBases[index].position;
        _inspectionCamera.gameObject.SetActive(true);
        _focused = true;
        _abilitiesWindow.FoldingBar.Toggle(true);
        _equipmentWindow.FoldingBar.Toggle(true);
    }

    public void LooseFocus()
    {
        if (_focused == false) return;
        _focused = false;
        _abilitiesWindow.FoldingBar.Toggle(false);
        _equipmentWindow.FoldingBar.Toggle(false);
        _inspectionCamera.SetActive(false);
    }
}
