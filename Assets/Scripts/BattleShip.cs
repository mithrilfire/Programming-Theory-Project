using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class BattleShip : Ship
{
    [SerializeField] float _attackCooldown = 1f;
    [SerializeField] float _damage = 500f;
    [SerializeField] ShipCannon _cannon;
    float _attackTimer;

    // POLYMORPHISM
    //Todo: Coroutines can be used for cooldown system.
    protected override void Action()
    {
        if (_target.Team == _team)
        {
            return;
        }
        else
        {
            _attackTimer += Time.deltaTime;

            if (_attackTimer >= _attackCooldown)
            {
                _cannon.Fire(_target.transform.position, _damage);
                _attackTimer = 0f;
            }
        }
    }
}
