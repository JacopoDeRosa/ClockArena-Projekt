using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitboxController : MonoBehaviour
{

    [SerializeField] private List<HitboxArea> _areas;
    [SerializeField] private List<LimbDamageMultiplier> _limbDamageMultipliers;

    private Guid _hitboxId;

    private List<ActiveDamage> _activeDamage;
    private List<ActiveDamage> _damageToRemove;

    public event System.Action<Damage> onDamage;

    private void Awake()
    {
        _hitboxId = Guid.NewGuid();
        _activeDamage = new List<ActiveDamage>();
        _damageToRemove = new List<ActiveDamage>();
        foreach (HitboxArea area in _areas)
        {
            area.SetId(_hitboxId);
            area.onHit += OnAreaHit;
        }
        var damageMultipliersSet = new HashSet<LimbDamageMultiplier>(_limbDamageMultipliers);
        _limbDamageMultipliers = new List<LimbDamageMultiplier>(damageMultipliersSet);
    }
    private void OnValidate()
    {
        FindAreas();     
        if (_limbDamageMultipliers != null)
        {
            var damageMultipliersSet = new HashSet<LimbDamageMultiplier>(_limbDamageMultipliers);
            if(damageMultipliersSet.Count != _limbDamageMultipliers.Count)
            {
                Debug.LogWarning("Warning: " + gameObject.name + "'s Hitbox component contains duplicate damage multipliers," +
                    " these will be scrubbed at runtime");
            }
        }

    }

    private void FindAreas()
    {
        var areas = GetComponentsInChildren<HitboxArea>();
        _areas = new List<HitboxArea>(areas);
    }
    private void OnAreaHit(HitboxEventData data)
    {
        if (_activeDamage.Find(x => x.Equals(data.Damage)) != null) return;

        _activeDamage.Add(new ActiveDamage(data.Damage));

        LimbDamageMultiplier damageMulti = _limbDamageMultipliers.Find(x => x.Equals(data.Bodypart));

        float multiplier = 1;

        if(damageMulti != null)
        {
            multiplier = damageMulti.Multiplier;
        }

        data.Damage.MultiplyDamage(multiplier);

        Debug.Log(name + " Has Taken: " + data.Damage.DamageAmount + " Damage with ID: " + data.Damage.ID);

        onDamage?.Invoke(data.Damage);
    }

    private void Update()
    {
        foreach (ActiveDamage item in _activeDamage)
        {
            if(item.UpdateLifetime())
            {
                _damageToRemove.Add(item);
            }
        }
        foreach (ActiveDamage item in _damageToRemove)
        {
            _activeDamage.Remove(item);
        }
        _damageToRemove.Clear();
    }
}

