using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;
using System.Collections;

public class CharacterAbilities : MonoBehaviour, IBarAction
{
    [SerializeField] private Character _user;
    [SerializeField] private Ability _primaryAbility, _secondaryAbility;
    [SerializeField] private AbilityTree _activeTree;


    public AbilityTree ActiveTree { get => _activeTree; }

    public Ability Primary { get => _primaryAbility; }
    public Ability Secondary { get => _secondaryAbility; }

    public event Action<Ability> onPrimaryChange, onSecondaryChange;
    public event Action onActionEnd;


    private WorldGizmos _worldGizmos;
    private PlayerInput _input;
    private MousePointGetter _mousePointGetter;

    private bool _targetAoe;
    private bool _targetCharacters;

    private Ability _activeAbility;

    private bool _busy;

    private void Awake()
    {
        _worldGizmos = FindObjectOfType<WorldGizmos>();
        _mousePointGetter = FindObjectOfType<MousePointGetter>();
    }

    private void Start()
    {
        _input = PlayerInputSingleton.Instance;
        if (_input)
        {
            _input.actions["Confirm"].started += OnConfirm;
        }
    }

    private void OnDestroy()
    {
        if (_input)
        {
            _input.actions["Confirm"].started -= OnConfirm;
        }
    }

    private void OnConfirm(InputAction.CallbackContext context)
    {
        if(_targetAoe)
        {
            if (_mousePointGetter.GetMousePoint(out Vector3 point))
            {
                _activeAbility.ActiveUse(_user, point);
                _user.Voice.PlayAttack();
            }
            if(_activeAbility.Duration > 0)
            {
              StartCoroutine(EndActionDelayed(_activeAbility.Duration));
            }
            else
            {
                EndAction();
            }
            
            _targetAoe = false;
            _worldGizmos.ResetRangeGizmo();
        }
        else if(_targetCharacters)
        {
            if(_mousePointGetter.GetHoveredObject(out GameObject hover) && hover.TryGetComponent(out Character character))
            {
                _activeAbility.ActiveUse(_user, character);
            }

            if (_activeAbility.Duration > 0)
            {
                StartCoroutine(EndActionDelayed(_activeAbility.Duration));
            }
            else
            {
                EndAction();
            }

            _targetCharacters = false;
        }
    }

    public void SetPrimaryAbility(AbilityDescriptor descriptor)
    {
        _primaryAbility = _activeTree.GetAbility(descriptor);
        onPrimaryChange?.Invoke(_primaryAbility);
    }
    public void SetSecondaryAbility(AbilityDescriptor descriptor)
    {
        _secondaryAbility = _activeTree.GetAbility(descriptor);
        onSecondaryChange?.Invoke(_secondaryAbility);
    }
    public void SetAbilityTree(int tree)
    {
        _activeTree = GameItemDB.GetDbOfType<AbilityTreesDB>().GetItem(tree);
    }

    private void Update()
    {
        if(_targetAoe &&  _mousePointGetter.GetMousePoint(out Vector3 point))
        {
            _worldGizmos.SetRangeGizmo(point, _activeAbility.Range);
        }
    }

    private void UseAbility(Ability ability)
    {
        _activeAbility = ability;

        if (ability.AbilityType == AbilityTypes.AreaOfEffect)
        {
            _targetAoe = true;
        }
        else if(ability.AbilityType == AbilityTypes.Other)
        {
            _targetCharacters = true;
        }
        else if(ability.AbilityType == AbilityTypes.Self)
        {
            _activeAbility.ActiveUse(_user);
            _user.Voice.PlayAcknowledge();
            onActionEnd?.Invoke();
        }


    }

    private void UsePrimaryAbility()
    {
        UseAbility(_primaryAbility);
    }

    private void UseSecondaryAbility()
    {
        UseAbility(_secondaryAbility);
    }

    private bool CancelActiveAbility()
    {
        if (_busy) return false;

        _targetAoe = false;
        _targetCharacters = false;
        _activeAbility = null;
        return true;
    }

    private void EndAction()
    {
        onActionEnd?.Invoke();
        _activeAbility = null;
    }

    private IEnumerator EndActionDelayed(float seconds)
    {
        _busy = true;
        yield return new WaitForSeconds(seconds);
        EndAction();
        _busy = false;
    }

    public IEnumerable<BarAction> GetBarActions()
    {
        if (_primaryAbility != null && _primaryAbility.HasActiveUse)
        {
            yield return new BarAction(UsePrimaryAbility, CancelActiveAbility, this, _primaryAbility.Name, _primaryAbility.Description, _primaryAbility.Icon);
        }
        if (_secondaryAbility != null && _secondaryAbility.HasActiveUse)
        {
            yield return new BarAction(UseSecondaryAbility, CancelActiveAbility, this, _secondaryAbility.Name, _secondaryAbility.Description, _secondaryAbility.Icon);
        }
    }
}
