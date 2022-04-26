using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Ship : MonoBehaviour
{
    [SerializeField] protected ShipTeam _team;
    [SerializeField] protected ShipClass _shipClass;
    protected uint _shipId; //Todo: Integrate ids for OnDestroyEvents
    protected float _health = 100f;
    protected NavMeshAgent _agent;
    [SerializeField] protected Ship _target;
    protected Coroutine _chaseCoroutine;
    protected AIState _currentState;
    public ShipTeam Team { get => _team; }
    public delegate void ShipAction(Ship ship);
    public static event ShipAction OnShipDestroy;

    public enum AIState
    {
        Chase,
        Action
    }
    public enum ShipTeam
    {
        Mercenary,
        Pirate
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.acceleration = _shipClass.Acceleration;
        _agent.angularSpeed = _shipClass.AngularSpeed;
        _agent.speed = _shipClass.MaxSpeed;
    }

    private void Start()
    {
        _health = _shipClass.MaxHealth;
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

                if (distance < _shipClass.ActionRange)
                {
                    _currentState = AIState.Action;
                    StopChase();
                }

                break;
            case AIState.Action:

                if (distance > _shipClass.ActionRange)
                {
                    _currentState = AIState.Chase;
                    _chaseCoroutine = StartCoroutine(ChaseTarget());
                }

                Action();
                break;
        }
    }
    public virtual void GoTo(Vector3 target)
    {
        StopChase();
        _currentState = AIState.Chase;
        _agent.isStopped = false;

        _target = null;
        _agent.SetDestination(target);
    }

    public virtual void GoTo(Ship target)
    {
        StopChase();
        _currentState = AIState.Chase;

        _target = target;
        _chaseCoroutine = StartCoroutine(ChaseTarget());
    }

    public virtual void TakeDamage(Ship attacker, float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            //! If gameobject has destroyed while selected, cursor has destroyed too because cursor is child of gameobject
            //* Custom destroying script or following cursor script without parenting
            //* Ship has been destroyed event ?
            OnShipDestroy?.Invoke(this);
        }

        Debug.Log(damage + " damage is recieved from " + attacker.name.ToString() + " to " + name.ToString());
    }

    protected void StopChase()
    {
        _agent.isStopped = true;

        if (_chaseCoroutine != null)
        {
            StopCoroutine(_chaseCoroutine);
            _chaseCoroutine = null;
        }
    }

    protected IEnumerator ChaseTarget()
    {
        _agent.isStopped = false;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            _agent.SetDestination(_target.transform.position);
        }
    }
    protected abstract void Action();
}