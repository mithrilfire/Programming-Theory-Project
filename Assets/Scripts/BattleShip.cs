using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleShip : Ship
{
    [SerializeField] float _attackCooldown = 1f;
    float _attackTimer;

    //Todo: Commander ship cant attack but battle ship (and maybe repair ship) can attack.
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
                _attackTimer = 0f;
                _target.TakeDamage(this, 500f);
            }
        }
    }
}
