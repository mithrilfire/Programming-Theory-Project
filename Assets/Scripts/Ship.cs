using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Ship : MonoBehaviour
{
    [SerializeField] float _actionRange = 3f;
    [SerializeField] Team _team;
    NavMeshAgent _agent;
    Ship _target;
    Coroutine _chaseCoroutine;
    AIState _currentState;
    public enum AIState
    {
        Chase,
        Action
    }
    public enum Team
    {
        Mercenary,
        Pirate
    }
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_target == null)
        {
            return;
        }
        float distance = Vector3.Distance(transform.position, _target.transform.position);
        switch (_currentState)
        {
            case AIState.Chase:

                if (distance < _actionRange)
                {
                    _currentState = AIState.Action;
                    StopChase();
                }

                break;
            case AIState.Action:

                if (distance > _actionRange)
                {
                    _currentState = AIState.Chase;
                    _chaseCoroutine = StartCoroutine(ChaseTarget());
                }

                //Call Action Method
                break;
        }
    }
    public void GoTo(Vector3 target)
    {
        StopChase();
        _currentState = AIState.Chase;
        _agent.isStopped = false;

        _target = null;
        _agent.SetDestination(target);
    }

    public void GoTo(Ship target)
    {
        StopChase();
        _currentState = AIState.Chase;

        _target = target;
        _chaseCoroutine = StartCoroutine(ChaseTarget());
    }

    void StopChase()
    {
        _agent.isStopped = true;

        if (_chaseCoroutine != null)
        {
            StopCoroutine(_chaseCoroutine);
            _chaseCoroutine = null;
        }
    }

    IEnumerator ChaseTarget()
    {
        _agent.isStopped = false;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            _agent.SetDestination(_target.transform.position);
        }
    }
}
