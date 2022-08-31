using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsUI : MonoBehaviour
{
    [SerializeField] private StatsBar _healthBar, _staminaBar, _apBar, _sanityBar;


    private Character _activeCharacter;
    private GameTurnManager _turnManager;

    private void Awake()
    {
        _turnManager = FindObjectOfType<GameTurnManager>();
        _turnManager.onNextCharacter.AddListener(SwitchCharacter);
    }

    private void SwitchCharacter(Character character)
    {
        if(_activeCharacter != null)
        {
            _activeCharacter.Stats.onHpChange -= _healthBar.SetValue;
            _activeCharacter.Stats.onStaminaChange -= _staminaBar.SetValue;
            _activeCharacter.Stats.onApChange -= _apBar.SetValue;
            _activeCharacter.Stats.onSanityChange -= _sanityBar.SetValue;
        }

        _activeCharacter = character;

        _healthBar.SetMaxValue(_activeCharacter.Stats.MaxHp);
        _staminaBar.SetMaxValue(_activeCharacter.Stats.MaxStamina);
        _apBar.SetMaxValue(_activeCharacter.Stats.MaxAp);
        _sanityBar.SetMaxValue(_activeCharacter.Stats.MaxSanity);

        _healthBar.SetValue(_activeCharacter.Stats.HP);
        _staminaBar.SetValue(_activeCharacter.Stats.Stamina);
        _apBar.SetValue(_activeCharacter.Stats.AP);
        _sanityBar.SetValue(_activeCharacter.Stats.Sanity);

        _activeCharacter.Stats.onHpChange += _healthBar.SetValue;
        _activeCharacter.Stats.onStaminaChange += _staminaBar.SetValue;
        _activeCharacter.Stats.onApChange += _apBar.SetValue;
        _activeCharacter.Stats.onSanityChange += _sanityBar.SetValue;
    }
}
