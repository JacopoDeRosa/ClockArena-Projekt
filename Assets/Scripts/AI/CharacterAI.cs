using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAI : MonoBehaviour, ISleeper
{
    [SerializeField] private Character _target;
    [SerializeField] private float _sightRange;
    [SerializeField] private float _loiterRadius = 10;
    [SerializeField] private float _engagementDistance = 5;
    [SerializeField] private LayerMask _sightMask;
    [SerializeField] private CharacterComponentsData _startData;


    private TurnEndAction _turnEndAction;
    private GameTurnManager _turnManager;

    private void Awake()
    {
        _target.GUI.ShowGui(true);

        _target.SetIsPlayer(false);

        _target.DataReader.ReadData(_startData);

        _turnEndAction = GetComponent<TurnEndAction>();

        _turnManager = FindObjectOfType<GameTurnManager>();

        _turnManager.AddCharacter(_target);
    }

    public void Sleep()
    {
       
    }

    public void WakeUp()
    {
        StartCoroutine(Behaviour());
    }

    private IEnumerator Behaviour()
    {
        yield return null;
        yield return null;
        yield return null;
        yield return MainBehaviour();
        yield return new WaitForSeconds(3);
        _turnEndAction.EndTurn();
    }

    protected virtual IEnumerator MainBehaviour()
    {
        List<Character> enemyCharacters = new List<Character>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, _sightRange, _sightMask);

        foreach (Collider collider in colliders)
        {
            if(collider.TryGetComponent(out Character character) && character.IsPlayerCharacter)
            {
                enemyCharacters.Add(character);
            }
        }

        if(enemyCharacters.Count == 0)
        {
            Vector3 randomDirection = Random.insideUnitSphere * _loiterRadius;
            randomDirection += transform.position;

            NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _loiterRadius, 1);
            yield return _target.Mover.MoveToPointRoutine(hit.position);
            yield break;
        }

        // Sort Closest Enemy
        for (int j = 0; j < 300; j++)
        {
            bool switched = false;

            for (int i = 0; i < enemyCharacters.Count - 1; i++)
            {
                Character a = enemyCharacters[i];
                Character b = enemyCharacters[i + 1];

                if(Vector3.SqrMagnitude(a.transform.position - transform.position) > Vector3.SqrMagnitude(b.transform.position - transform.position))
                {
                    enemyCharacters[i] = b;
                    enemyCharacters[i + 1] = a;
                    switched = true;
                }
            }

            yield return null;

            if(switched == false)
            {
                break;
            }
        }

        Character closestEnemy = enemyCharacters[0];

        if (_target.Equipment.Weapon is RangedWeapon)
        {
            if (Vector3.Distance(transform.position, closestEnemy.transform.position) > _engagementDistance)
            {
                NavMesh.SamplePosition(closestEnemy.transform.TransformPoint(new Vector3(0, 0, _engagementDistance)), out NavMeshHit hit, _engagementDistance, 1);

                yield return _target.Mover.MoveToPointRoutine(hit.position);
            }

            yield return ShootRoutine(closestEnemy.transform.position + new Vector3(0, 1.4f, 0));
        }
    }

    protected IEnumerator ShootRoutine(Vector3 point)
    {
        _target.Animator.SetBool("Aiming", true);

        _target.AimController.SetAimPointPosition(point);

        yield return _target.AimController.StartAimRoutine();
    
        yield return new WaitForSeconds(1.5f);

        _target.Equipment.Weapon.Attack();

        _target.Animator.SetTrigger("Shoot");

        _target.Animator.SetBool("Aiming", false);

        yield return new WaitForSeconds(_target.Equipment.Weapon.AttackTime);

        yield return _target.AimController.StopAimRoutine();
    }

}
