using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeBar : MonoBehaviour
{


    [SerializeField]
    private InitiativeBarSlot[] _slots = new InitiativeBarSlot[24];
    [SerializeField]
    private TurnInitiativeCalculator _initiativeCalculator;

    private void Awake()
    {
        _initiativeCalculator.onInitiaviteRolled.AddListener(SetInitiativeSlotCharacters);
    }


    public void SetInitiativeSlotCharacters(Character[] characters)
    {
        ToggleAllSlots(true);

        int overflow = (_slots.Length - characters.Length) - 1;
        for (int i = 0; i < characters.Length; i++)
        {
            _slots[i].SetCharacter(characters[i]);
        }
        if (overflow > 0)
        {
            for (int x = overflow; x < _slots.Length; x++)
            {
                _slots[x].gameObject.SetActive(false);         
            }
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
