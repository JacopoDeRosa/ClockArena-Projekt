using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private CharacterBaseStats _baseStats;
    [SerializeField] private HitboxController _hitbox;

    [SerializeField]
    private int _hp, _stamina, _ap, _sanity;
    private int _maxHp, _maxStamina, _maxAp, _maxSanity;

    public int HP { get => _hp; }
    public int Stamina { get => _stamina; }
    public int AP { get => _ap; }
    public int Sanity { get => _sanity; }

    public int MaxHp { get => _maxHp; }
    public int MaxAp { get => _maxAp; }
    public int MaxSanity { get => _maxSanity; }
    public int MaxStamina { get => _maxStamina; }


    public event Action<int> onHpChange, onStaminaChange, onApChange, onSanityChange;

    private void Start()
    {
        _hitbox.onDamage += DealDamage;
    }

    public void DealDamage(Damage damage)
    {
        _hp -= damage.DamageAmount;
        _hp = Mathf.Clamp(_hp, 0, _maxHp);
        onHpChange?.Invoke(_hp);
    }

    public void HealDamage(int damage)
    {
        _hp += damage;
        _hp = Mathf.Clamp(_hp, 0, _maxHp);
        onHpChange?.Invoke(_hp);
    }

    public void SetBaseStats(int dataType)
    {
        _baseStats = GameItemDB.GetDbOfType<DataDB>().GetItem(dataType);
        _maxHp = _baseStats.BaseHP;
        _maxAp = _baseStats.BaseAP;
        _maxSanity = _baseStats.BaseSanity;
        _maxStamina = _baseStats.BaseStamina;
        _hp = _maxHp;
        _ap = _maxAp;
        _sanity = _maxSanity;
        _stamina = _maxStamina;
    }

}
