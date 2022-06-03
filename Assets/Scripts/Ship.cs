using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Ship : MonoBehaviour
{
    [SerializeField] protected ShipTeam _team;
    [SerializeField] protected ShipClass _shipClass;
    [SerializeField] protected Animator _animator;
    protected float _health = 100f;
    protected NavMeshAgent _agent;
    protected Ship _target;
    protected Coroutine _chaseCoroutine;
    protected AIState _currentState;
    // ENCAPSULATION
    public ShipTeam Team { get => _team; }
    public delegate void ShipAction(ShipInfo info);
    public static event ShipAction OnShipDestroy;
    bool _isShipSank = false;

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
        if (_isShipSank)
        {
            if (_animator.GetCurrentAnimatorStateInfo(2).IsName("Sank"))
            {
                Destroy(gameObject);
            }
        }
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

    // POLYMORPHISM (Overloading)
    public virtual void GoTo(Vector3 target)
    {
        StopChase();
        _currentState = AIState.Chase;
        _agent.isStopped = false;

        _target = null;
        _agent.SetDestination(target);
    }

    // POLYMORPHISM (Overloading)
    public virtual void GoTo(Ship target)
    {
        StopChase();
        _currentState = AIState.Chase;

        _target = target;
        _chaseCoroutine = StartCoroutine(ChaseTarget());
    }
    public virtual bool TakeHealing(float healingRate)
    {
        if (_health >= _shipClass.MaxHealth || _isShipSank)
        {
            return false;
        }

        _health += healingRate;

        if (_health > _shipClass.MaxHealth)
        {
            _health = _shipClass.MaxHealth;
        }

        Debug.Log("Health : " + _health + " / " + _shipClass.MaxHealth);
        return true;
    }

    // ABSTRACTION
    public virtual void TakeDamage(float damage, Vector3 pos, ShipTeam team)
    {
        if (team == _team)
            return;

        float hitSide = Mathf.Sign((transform.position - pos).x);
        _animator.SetFloat("HitSide", hitSide);
        _animator.SetTrigger("Hit");
        _health -= damage;

        if (_health <= 0 && !_isShipSank)
        {
            _isShipSank = true;
            OnShipDestroy?.Invoke(new ShipInfo(_team, _shipClass.IsItMainShip));
            _animator.SetBool("IsShipSank", true);
        }

        Debug.Log("Health : " + _health + " / " + _shipClass.MaxHealth);
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

    public class ShipInfo
    {
        public ShipTeam Team { get; private set; }
        public bool IsItMainShip { get; private set; }

        public ShipInfo(ShipTeam team, bool isItMainShip)
        {
            Team = team;
            IsItMainShip = isItMainShip;
        }
    }
}