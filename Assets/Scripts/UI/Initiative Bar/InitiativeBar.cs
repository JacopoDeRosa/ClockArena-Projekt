using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeBar : MonoBehaviour
{


    [SerializeField]
    private InitiativeBarSlot[] _slots = new InitiativeBarSlot[24];
    [SerializeField]
    private TurnInitiativeCalculator _initiativeCalculator;
    [SerializeField]
    private WorldGizmos _worldGizmos; 

    private void Awake()
    {
        _initiativeCalculator.onInitiaviteRolled.AddListener(SetInitiativeSlotCharacters);
    }


    private void SetInitiativeSlotCharacters(Character[] characters)
    {
        ToggleAllSlots(false);
        for (int i = 0; i < characters.Length; i++)
        {
            _slots[i].gameObject.SetActive(true);
            _slots[i].SetCharacter(characters[i]);
        }
    }
    private void ToggleAllSlots(bool status)
    {
        foreach (var slot in _slots)
        {
            slot.gameObject.SetActive(status);
        }
    }
}
