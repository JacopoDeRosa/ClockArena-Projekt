using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAttackAction : MonoBehaviour, IBarAction
{
    [SerializeField] private Character _user;
    [SerializeField] private LayerMask _aimingMask;
    [SerializeField] private float _aimHardness = 3;

    private MousePointGetter _mousePointGetter;
    private PlayerInput _input;
    private WorldGizmos _worldGizmos;

    private bool _aiming;
    private bool _shooting;
    private Vector3 _aimPosition;

    public event Action onActionEnd;
    private IconsDB _iconsDB;

    private void Awake()
    {
        _mousePointGetter = FindObjectOfType<MousePointGetter>();
        _worldGizmos = FindObjectOfType<WorldGizmos>();
        _iconsDB = GameItemDB.GetDbOfType<IconsDB>();
    }

    private void Start()
    {
      
        _input = FindObjectOfType<PlayerInput>();
        if(_input)
        {
            _input.actions["Confirm"].started += OnConfirm;
        }
    }

    private void OnDisable()
    {
        if(_input)
        {
            _input.actions["Confirm"].started -= OnConfirm;
        }
    }



    public void Punch()
    {
        _user.Animator.SetTrigger("Melee");
        _user.Voice.PlayAttack();
        StartCoroutine(EndActionDelayed(2));
    }

    public void Melee()
    {
        _user.Animator.SetTrigger("Melee");
        _user.Voice.PlayAttack();
        _user.Equipment.Weapon.Attack();
        StartCoroutine(EndActionDelayed(2));
    }

    public bool NoCancel()
    {
        return false;
    }

    public void StartAiming()
    {
        if(_user.Mover.CurrentStance == Stance.Prone)
        {
            onActionEnd?.Invoke();
            return;
        }
        _worldGizmos.SetPointerPosition(Vector3.zero);
        _aiming = true;
        _user.Animator.SetBool("Aiming", true);
        _user.AimController.StartAiming();
    }

    public bool CancelAim()
    {
        if (_shooting) return false;

        _aiming = false;
        _user.Animator.SetBool("Aiming", false);
        _worldGizmos.ResetPointer();
        _user.AimController.StopAiming();
        return true;
    }

    private void Update()
    {
        if(_aiming)
        {
            if(_mousePointGetter.GetMousePoint(out Vector3 point, _aimingMask))
            {
                _worldGizmos.SetPointerPosition(point);

                _aimPosition = point;

                Vector3 aimPoint = Vector3.Lerp(_user.AimController.AimPointPosition, point, _aimHardness * Time.deltaTime);

                _user.AimController.SetAimPointPosition(aimPoint);
            }
        }
    }

    private IEnumerator EndActionDelayed(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        onActionEnd?.Invoke();
    }

    private IEnumerator ShootRoutine()
    {

        _shooting = true;

        _aiming = false;

        _user.Equipment.Weapon.Attack();

        _user.Animator.SetTrigger("Shoot");

        _user.Animator.SetBool("Aiming", false);

        _worldGizmos.ResetPointer();

        yield return new WaitForSeconds(_user.Equipment.Weapon.ShotTime);

        _shooting = false;

        onActionEnd?.Invoke();

        _user.AimController.StopAiming();
    }

    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (_aiming && _shooting == false)
        {
            if (_mousePointGetter.GetMousePoint(out Vector3 point, _aimingMask))
            {
                StartCoroutine(ShootRoutine());
            }
        }
    }

    public IEnumerable<BarAction> GetBarActions()
    {
        if(_user.Equipment.Weapon is RangedWeapon)
        {
            yield return new BarAction(StartAiming, CancelAim, this, _user.Equipment.Weapon.Data.Name, "Shoot Your Ranged Weapon", _iconsDB.ShootSprite);
        }
        else if (_user.Equipment.Weapon is MeleeWeapon)
        {
            yield return new BarAction(Melee, NoCancel, this, _user.Equipment.Weapon.Data.Name, "Attack with your Melee Weapon", _iconsDB.MeleeSprite);
        }
        else if(_user.Equipment.Weapon == null)
        {
            yield return new BarAction(Punch, NoCancel, this, "Punch", "Punch anything in front of you", _iconsDB.MeleeSprite);
        }
    }
}
